using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth = 100f;
    public GameObject model;
    public Sprite lifeBarSafe;
    public Sprite lifeBarDamaged;
    public GameObject ptcBloodPref;

    private RectTransform lifeBar;

    private void Awake()
    {
        lifeBar = GameObject.Find("LifeBar").transform.GetChild(1).GetComponent<RectTransform>();
    }

    public void StartDoor()
    {
        StartCoroutine(OpenFirstDoors());
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;
    }

    public void SetHealth(float _health)
    {
        if (_health > health)
        {
            health = _health;

            StartCoroutine(LifeBarAnimRegen());
        }

        if (_health < health)
        {
            health = _health;

            GameObject ptcBlood;
            ptcBlood = Instantiate(ptcBloodPref, transform.position, Quaternion.identity);
            Destroy(ptcBlood, 4f);

            GameObject.Find("ClientManager").GetComponent<SoundManager>().playAudioClip(2);

            StartCoroutine(LifeBarAnim());
        }

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        model.SetActive(false);

        GameObject.Find("ClientManager").GetComponent<SoundManager>().playAudioClip(7);
    }

    public void Respawn()
    {
        //model.enabled = true;
        SetHealth(maxHealth);
    }

    IEnumerator LifeBarAnim()
    {
        lifeBar.gameObject.GetComponent<Image>().sprite = lifeBarDamaged;

        float sizeToReach = health * 3;

        while(lifeBar.sizeDelta.x > sizeToReach)
        {
            yield return new WaitForSeconds(0.01f);

            lifeBar.sizeDelta = new Vector2(lifeBar.sizeDelta.x - 2, 60);
        }

        lifeBar.gameObject.GetComponent<Image>().sprite = lifeBarSafe;
    }

    IEnumerator LifeBarAnimRegen()
    {
        float sizeToReach = health * 3;

        while (lifeBar.sizeDelta.x < sizeToReach)
        {
            yield return new WaitForSeconds(0.01f);

            lifeBar.sizeDelta = new Vector2(lifeBar.sizeDelta.x + 2, 60);
        }
    }

    IEnumerator OpenFirstDoors()
    {
        yield return new WaitForSeconds(40f);

        Transform Door01 = GameObject.Find("Door01").transform;
        Transform Door02 = GameObject.Find("Door02").transform;

        int i = 0;

        while (i < 100)
        {
            yield return new WaitForSeconds(0.01f);

            Door01.position = new Vector3(Door01.position.x - 0.1f, Door01.position.y, Door01.position.z);
            Door02.position = new Vector3(Door02.position.x + 0.1f, Door02.position.y, Door02.position.z);

            i++;
        }
    }
}