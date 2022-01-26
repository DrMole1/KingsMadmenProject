using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class IntroManager : MonoBehaviour
{
    public Image[] borders;
    public TMP_Text[] texts;
    public Image btnNext;
    public GameObject[] bgSpells;
    public Image[] imgSpells;
    public TMP_Text[] textSpells;
    public Image btnQuit;
    public GameObject intro;
    public AudioClip mainTheme;
    public GameObject textStory;
    public GameObject ptc;
    public GameObject generator01;
    public GameObject generator02;
    public GameObject enemyManager;

    private int state = 0;

    public void StartIntro()
    {
        GetComponent<AudioSource>().Play();

        StartCoroutine(ShowBorders());
    }

    IEnumerator ShowBorders()
    {
        while (borders[0].color.a < 1)
        {
            for(int i = 0; i < borders.Length; i++)
            {
                borders[i].color = new Color(1, 1, 1, borders[i].color.a + 0.01f);
            }

            yield return new WaitForSeconds(0.02f);
        }

        while (texts[0].alpha < 1)
        {
            texts[0].alpha += 0.01f;

            yield return new WaitForSeconds(0.02f);
        }

        while (btnNext.color.a < 1)
        {
            btnNext.color = new Color(1, 1, 1, btnNext.color.a + 0.01f);

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void NextState()
    {
        state++;

        if(state == 1)
        {
            btnNext.color = new Color(1, 1, 1, 0);
            StartCoroutine(ShowText2());
        }
        else if (state == 2)
        {
            btnNext.color = new Color(1, 1, 1, 0);
            StartCoroutine(ShowText3());
        }
        else if (state == 3)
        {
            btnNext.color = new Color(1, 1, 1, 0);
            StartCoroutine(ShowSpell());
        }
    }

    IEnumerator ShowText2()
    {
        while (texts[1].alpha < 1)
        {
            texts[1].alpha += 0.01f;

            yield return new WaitForSeconds(0.02f);
        }

        while (btnNext.color.a < 1)
        {
            btnNext.color = new Color(1, 1, 1, btnNext.color.a + 0.01f);

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ShowText3()
    {
        while (texts[2].alpha < 1)
        {
            texts[2].alpha += 0.01f;

            yield return new WaitForSeconds(0.02f);
        }

        while (btnNext.color.a < 1)
        {
            btnNext.color = new Color(1, 1, 1, btnNext.color.a + 0.01f);

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ShowSpell()
    {
        textStory.SetActive(false);

        for (int i = 0; i < bgSpells.Length; i++)
        {
            bgSpells[i].SetActive(true);
        }

        while (imgSpells[0].color.a < 1)
        {
            for (int i = 0; i < imgSpells.Length; i++)
            {
                imgSpells[i].color = new Color(1, 1, 1, imgSpells[i].color.a + 0.01f);
            }

            yield return new WaitForSeconds(0.01f);
        }

        while (textSpells[0].alpha < 1)
        {
            for (int i = 0; i < textSpells.Length; i++)
            {
                textSpells[i].alpha += 0.01f;
            }

            yield return new WaitForSeconds(0.01f);
        }

        while (btnQuit.color.a < 1)
        {
            btnQuit.color = new Color(1, 1, 1, btnQuit.color.a + 0.01f);

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void QuitIntro()
    {
        intro.SetActive(false);

        ptc.SetActive(true);
        generator01.SetActive(true);
        generator02.SetActive(true);
        enemyManager.SetActive(true);

        GameObject.Find("LocalPlayer(Clone)").GetComponent<PlayerManager>().StartDoor();

        GetComponent<AudioSource>().clip = mainTheme;
        GetComponent<AudioSource>().Play();
    }
}
