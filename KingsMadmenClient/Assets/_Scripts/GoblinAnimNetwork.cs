using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimNetwork : MonoBehaviour
{
	private Animator anim;
	private bool canMove = true;



	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		anim.SetInteger("battle", 1);
	}

	// Update is called once per frame
	void Update()
	{
		/*
		//Debug.Log(anim.GetInteger("moving"));

		// MOVING
		if (canMove) // moving
		{
			if (Input.GetKey("z") || Input.GetKey("s") || Input.GetKey("q") || Input.GetKey("d"))
			{
				anim.SetInteger("moving", 2); // run	
			}
			else
			{
				anim.SetInteger("moving", 0); // iddle
			}
		}

		// DEFENCE
		if (Input.GetMouseButtonDown(1)) // defence_start
		{
			anim.SetInteger("moving", 8);
			canMove = false;
		}
		if (Input.GetMouseButtonUp(1))  // defence_end
		{
			anim.SetInteger("moving", 9);
			canMove = true;
		}

		// ATTACK
		if (Input.GetMouseButton(0)) //attack
		{
			anim.SetInteger("moving", 3);
			canMove = false;
		}
		if (Input.GetMouseButtonUp(0)) //attack
		{
			canMove = true;
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
			int n = Random.Range(0, 2);
			if (n == 0)
			{
				anim.SetInteger("moving", 10);
			}
			else
			{
				anim.SetInteger("moving", 11);
			}
		}
		*/
	}

	public void Move()
	{
		anim.SetInteger("moving", 2); // run	
	}

	public void Iddle()
	{
		anim.SetInteger("moving", 0); // iddle	
	}
}
