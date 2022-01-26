using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Goblin_ro_ctrl : MonoBehaviour {

	public Sprite[] iconSprites;
	public GameObject mortalGlyphePref;
	public GameObject ptcTeleportationPref;

	private Animator anim;
	private bool canMove = true;
	private RectTransform spell01;
	private RectTransform spell02;
	private bool isFilling01 = false;
	private bool isFilling02 = false;
	private Image spellIcon01;
	private Image spellIcon02;
	private bool powerUp1 = false;
	private bool powerUp2 = false;
	private GameObject mortalGlyphe;



	// Use this for initialization
	void Start () 
	{	
		anim = GetComponent<Animator>();
		anim.SetInteger("battle", 1);

		spell01 = GameObject.Find("SpellIcon01").transform.GetChild(1).GetComponent<RectTransform>();
		spell02 = GameObject.Find("SpellIcon02").transform.GetChild(1).GetComponent<RectTransform>();

		spellIcon01 = GameObject.Find("SpellIcon01").transform.GetChild(2).GetComponent<Image>();
		spellIcon02 = GameObject.Find("SpellIcon02").transform.GetChild(2).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log(anim.GetInteger("moving"));

		// MOVING
		if (canMove) // moving
		{
			if(Input.GetKey("z") || Input.GetKey("s") || Input.GetKey("q") || Input.GetKey("d"))
			{
				anim.SetInteger("moving", 2); // run	
			}
			else
			{
				anim.SetInteger("moving", 0); // iddle
			}			
		} 

		// ATTACK
		if (Input.GetMouseButtonDown (0)) //attack
		{
			anim.SetInteger("moving", 3);
			canMove = false;
			isFilling01 = true;
			StartCoroutine(FillSpell01());
		}
		if (Input.GetMouseButtonUp(0)) //attack
		{
			canMove = true;
			isFilling01 = false;
			powerUp1 = false;
			spell01.sizeDelta = new Vector2(200, 0);
			spellIcon01.sprite = iconSprites[0];
		}

		// DEFENCE
		if (Input.GetMouseButtonDown(1)) // defence_start
		{
			anim.SetInteger("moving", 8);
			canMove = false;
			isFilling02 = true;
			StartCoroutine(FillSpell02());
		}
		if (Input.GetMouseButtonUp(1))  // defence_end
		{
			anim.SetInteger("moving", 9);
			canMove = true;
			isFilling02 = false;
			powerUp2 = false;
			spell02.sizeDelta = new Vector2(200, 0);
			spellIcon02.sprite = iconSprites[2];
		}

		// JUMP
		if (Input.GetKeyDown("space")) //jump
		{
			anim.SetInteger("moving", 7);
			canMove = false;
		}
		if (Input.GetKeyUp("space")) //jump
		{
			canMove = true;
		}


		if (Input.GetKeyDown("o")) //die_1
		{
			anim.SetInteger("moving", 12);
		}
		if (Input.GetKeyDown("i")) //die_2
		{
			anim.SetInteger("moving", 13);
		}

		if (Input.GetKeyDown("m")) // teleportation
		{
			anim.SetInteger("moving", 6);
		}

		if (Input.GetKeyDown("u")) // hit
		{
			int n = Random.Range(0,2);
			if (n == 0) 
			{
				anim.SetInteger("moving", 10);
			} 
			else 
			{
				anim.SetInteger("moving", 11);
			}
		}
	}

	IEnumerator FillSpell01()
	{
		while(isFilling01 && spell01.sizeDelta.y < 200)
		{
			yield return new WaitForSeconds(0.01f);

			spell01.sizeDelta = new Vector2(200, spell01.sizeDelta.y + 4f);

			if (spell01.sizeDelta.y > 195)
			{
				spellIcon01.sprite = iconSprites[1];

				if(!powerUp1)
				{
					powerUp1 = true;
					ClientSend.PlayerTeleportation();

					yield return new WaitForSeconds(0.05f);

					if(mortalGlyphe != null)
					{
						GameObject.Find("ClientManager").GetComponent<SoundManager>().playAudioClip(5);

						GameObject.Find("LocalPlayer(Clone)").GetComponent<PlayerController>().isBoosted = true;
					}

					yield return new WaitForSeconds(0.12f);

					if (mortalGlyphe != null)
					{
						GameObject ptcTeleportation;
						ptcTeleportation = Instantiate(ptcTeleportationPref, new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z), Quaternion.identity);
						Destroy(ptcTeleportation, 4f);

						Destroy(mortalGlyphe);
					}
				}
			}
		}

		if(!isFilling01)
		{
			spell01.sizeDelta = new Vector2(200, 0);
			powerUp1 = false;
		}
	}

	IEnumerator FillSpell02()
	{
		while (isFilling02 && spell02.sizeDelta.y < 200)
		{
			yield return new WaitForSeconds(0.01f);

			spell02.sizeDelta = new Vector2(200, spell02.sizeDelta.y + 4f);

			if(spell02.sizeDelta.y > 195)
			{
				spellIcon02.sprite = iconSprites[3];

				if (!powerUp2)
				{
					powerUp2 = true;
					ClientSend.PlayerPutGlyphe();

					GameObject.Find("ClientManager").GetComponent<SoundManager>().playAudioClip(4);

					if (GameObject.Find("MortalGlyphe(Clone)") != null)
					{
						Destroy(GameObject.Find("MortalGlyphe(Clone)"));
					}

					mortalGlyphe = Instantiate(mortalGlyphePref, transform.position, Quaternion.identity);
				}
			}
		}

		if (!isFilling02)
		{
			spell02.sizeDelta = new Vector2(200, 0);
			powerUp2 = false;
		}
	}
}

