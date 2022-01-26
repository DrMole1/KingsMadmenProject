using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private float speed;

    public void StartToDown()
    {
        speed = Random.Range(2, 6);

        StartCoroutine(Down());
    }

    public void StartToUp()
    {
        speed = Random.Range(2, 6);

        StartCoroutine(Up());
    }

    IEnumerator Down()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (0.01f * speed), transform.position.z);

        yield return new WaitForSeconds(0.01f);

        StartCoroutine(Down());
    }

    IEnumerator Up()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (0.01f * speed), transform.position.z);

        yield return new WaitForSeconds(0.01f);

        StartCoroutine(Up());
    }
}
