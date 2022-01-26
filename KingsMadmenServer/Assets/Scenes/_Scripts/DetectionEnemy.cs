using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEnemy : MonoBehaviour
{
    public bool isPlayer1Seen = false;
    public bool isPlayer2Seen = false;
    public Enemy enemy;

    public Transform player1;
    public Transform player2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.isAlreadySeen = true;

            Debug.Log("Player seen");
            if (other.GetComponent<Player>().id == 1)
            {
                Debug.Log("Player 1 seen");
                isPlayer1Seen = true;
                player1 = other.transform;

                if (!isPlayer2Seen)
                {
                    FocusPlayer1();
                }
            }
            else
            {
                isPlayer2Seen = true;
                player2 = other.transform;

                if (!isPlayer1Seen)
                {
                    FocusPlayer2();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().id == 1)
            {
                isPlayer1Seen = false;

                if (isPlayer2Seen)
                {
                    FocusPlayer2();
                }
                else
                {
                    UnFocus();
                }
            }
            else
            {
                isPlayer2Seen = false;

                if (isPlayer1Seen)
                {
                    FocusPlayer1();
                }
                else
                {
                    UnFocus();
                }
            }
        }
    }

    private void FocusPlayer1()
    {
        enemy.currentTarget = player1;
    }

    private void FocusPlayer2()
    {
        enemy.currentTarget = player2;
    }

    private void UnFocus()
    {
        enemy.currentTarget = null;
    }
}
