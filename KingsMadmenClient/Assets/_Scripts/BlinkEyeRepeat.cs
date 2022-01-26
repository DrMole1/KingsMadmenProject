using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEyeRepeat : MonoBehaviour
{
    // =============== VARIABLES ===============

    public float DELAY = 3f;
    public bool isStuck = false;

    private const float MICRODELAY = 0.66f;
    private Animator eye;
    private Transform player;
    private float maxScale;

    // ========================================


    public void Start()
    {
        maxScale = transform.localScale.x;
        transform.localScale = new Vector2(0f, 0f);
        StartCoroutine(GrowEye());

        eye = GetComponent<Animator>();

        StartCoroutine(BlinkEye());
    }

    IEnumerator BlinkEye()
    {
        if(player == null && GameObject.Find("LocalPlayer(Clone)") != null)
        {
            player = GameObject.Find("LocalPlayer(Clone)").transform;
        }

        yield return new WaitForSeconds(DELAY);

        eye.SetBool("CanBlink", true);

        yield return new WaitForSeconds(MICRODELAY);

        eye.SetBool("CanBlink", false);

        StartCoroutine(BlinkEye());
    }

    private void Update()
    {
        if(player != null && !isStuck)
        {
            transform.LookAt(player);
        }
    }

    IEnumerator GrowEye()
    {
        yield return new WaitForSeconds(4f);

        while (transform.localScale.x < maxScale)
        {
            transform.localScale = new Vector2(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
