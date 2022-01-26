using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHit : MonoBehaviour
{
    public bool canHitPlayer1 = false;
    public bool canHitPlayer2 = false;
    public Enemy enemy;



    private void Awake()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().id == 1)
            {
                canHitPlayer1 = true;
            }
            else
            {
                canHitPlayer2 = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().id == 1)
            {
                canHitPlayer1 = false;
            }
            else
            {
                canHitPlayer2 = false;
            }
        }
    }
}
