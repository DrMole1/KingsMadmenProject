using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBlood : MonoBehaviour
{
    public GameObject[] bloods;
    public bool isUp;
    public Transform pool;

    private void Start()
    {
        StartCoroutine(GenerateBlood());
    }

    IEnumerator GenerateBlood()
    {
        float time = Random.Range(0.8f, 1.2f);

        yield return new WaitForSeconds(time);

        int choice = Random.Range(0, 5);
        float x = Random.Range(transform.position.x - (transform.localScale.x / 2), transform.position.x + (transform.localScale.x / 2));
        float z = Random.Range(transform.position.z - (transform.localScale.z / 2), transform.position.z + (transform.localScale.z / 2));

        GameObject bloodObject;
        bloodObject = Instantiate(bloods[choice], new Vector3(x, transform.position.y, z), Quaternion.identity, pool);
        Destroy(bloodObject, 60f);

        if(isUp)
        {
            bloodObject.GetComponent<Blood>().StartToDown();
        }
        else
        {
            bloodObject.GetComponent<Blood>().StartToUp();
        }

        StartCoroutine(GenerateBlood());
    }
}
