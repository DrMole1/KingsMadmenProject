using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public int id;
    public Transform blockToMove;

    private int playerInCollision = 0;
    private bool isActivated = false;
    private float startPos;


    private void Start()
    {
        if(id == 0)
        {
            startPos = blockToMove.position.y;
        }
        else
        {
            startPos = blockToMove.position.z;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollision++;

            Check();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollision--;

            Check();
        }
    }

    public void Check()
    {
        if(playerInCollision > 0)
        {
            isActivated = true;

            if(id == 0)
            {
                StartCoroutine(DoorUp());
            }
            else
            {
                StartCoroutine(BridgeUp());
            }
        }
        else
        {
            isActivated = false;

            if (id == 0)
            {
                StartCoroutine(DoorDown());
            }
            else
            {
                StartCoroutine(BridgeDown());
            }
        }
    }

    IEnumerator DoorUp()
    {
        while(blockToMove.position.y < startPos + 10 && isActivated)
        {
            blockToMove.position = new Vector3(blockToMove.position.x, blockToMove.position.y + 0.1f, blockToMove.position.z);

            yield return new WaitForSeconds(0.01f);

            ServerSend.UpdateBlock(id, blockToMove.position);
        }
    }

    IEnumerator DoorDown()
    {
        while (blockToMove.position.y > startPos && !isActivated)
        {
            blockToMove.position = new Vector3(blockToMove.position.x, blockToMove.position.y - 0.1f, blockToMove.position.z);

            yield return new WaitForSeconds(0.01f);

            ServerSend.UpdateBlock(id, blockToMove.position);
        }
    }
    IEnumerator BridgeUp()
    {
        while (blockToMove.position.z > startPos - 10 && isActivated)
        {
            blockToMove.position = new Vector3(blockToMove.position.x, blockToMove.position.y, blockToMove.position.z - 0.1f);

            yield return new WaitForSeconds(0.01f);

            ServerSend.UpdateBlock(id, blockToMove.position);
        }
    }

    IEnumerator BridgeDown()
    {
        while (blockToMove.position.z < startPos && !isActivated)
        {
            blockToMove.position = new Vector3(blockToMove.position.x, blockToMove.position.y, blockToMove.position.z + 0.1f);

            yield return new WaitForSeconds(0.01f);

            ServerSend.UpdateBlock(id, blockToMove.position);
        }
    }
}
