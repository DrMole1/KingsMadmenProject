using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().isBackOnEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().isBackOnEnemy = false;
        }
    }
}
