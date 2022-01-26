using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;
    public GameObject gameMenu;

    public Transform firstCam;
    public Image curtain;
    public Image title;
    public Image btn1;
    public Image btn2;

    private Vector3 currentRot;
    private Quaternion currentQuaternionRot;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        StartCoroutine(Intro());
    }

    /// <summary>Attempts to connect to the server.</summary>
    public void ConnectToServer()
    {
        startMenu.SetActive(false);

        gameMenu.SetActive(true);
        gameMenu.GetComponent<IntroManager>().StartIntro();

        usernameField.interactable = false;
        Client.instance.ConnectToServer();
    }

    IEnumerator Intro()
    {
        curtain.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(3f);

        while(curtain.color.a > 0)
        {
            curtain.color = new Color(0, 0, 0, curtain.color.a - 0.01f);

            yield return new WaitForSeconds(0.07f);
        }

        curtain.gameObject.SetActive(false);

        while (title.color.a < 1)
        {
            title.color = new Color(1, 1, 1, title.color.a + 0.01f);

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(4f);

        StartCoroutine(CamTraveling());

        yield return new WaitForSeconds(1f);

        while (btn1.color.a < 1)
        {
            btn1.color = new Color(1, 1, 1, btn1.color.a + 0.01f);

            yield return new WaitForSeconds(0.01f);
        }

        while (btn2.color.a < 1)
        {
            btn2.color = new Color(1, 1, 1, btn2.color.a + 0.01f);

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator CamTraveling()
    {
        firstCam.position = new Vector3(0f, 10.5f, 93f);
        currentRot = new Vector3(14f, 0f, 0f);
        currentQuaternionRot.eulerAngles = currentRot;
        firstCam.localRotation = currentQuaternionRot;

        while(firstCam.position.z > 50f)
        {
            firstCam.position = new Vector3(0f, 10.5f, firstCam.position.z - 0.1f);

            yield return new WaitForSeconds(0.01f);
        }

        firstCam.position = new Vector3(63f, 23f, 120f);
        currentRot = new Vector3(30f, -90f, 0f);
        currentQuaternionRot.eulerAngles = currentRot;
        firstCam.localRotation = currentQuaternionRot;

        while (firstCam.position.z > 77f)
        {
            firstCam.position = new Vector3(63f, 23f, firstCam.position.z - 0.1f);

            yield return new WaitForSeconds(0.01f);
        }

        if (!startMenu.activeSelf)
        {
            firstCam.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(CamTraveling());
        }
    }
}