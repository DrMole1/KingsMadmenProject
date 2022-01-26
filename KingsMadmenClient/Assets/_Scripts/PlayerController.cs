using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform camTransform;
    public bool isBoosted = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ClientSend.PlayerShoot(camTransform.forward);

            if (Physics.Raycast(transform.position, camTransform.forward, out RaycastHit _hit, 4f))
            {
                if (_hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("chill");
                    if (!isBoosted)
                    {
                        int choice = Random.Range(0, 2);
                        float pitch = Random.Range(0.8f, 1.2f);

                        GameObject.Find("ClientManager").GetComponent<SoundManager>().playAudioClipWithPitch(choice, pitch);
                    }
                    else
                    {
                        GameObject.Find("ClientManager").GetComponent<SoundManager>().playAudioClip(3);
                        isBoosted = false;
                    }
                }
            }      
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ClientSend.PlayerShield(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ClientSend.PlayerShield(false);
        }
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    /// <summary>Sends player input to the server.</summary>
    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.Z),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.Q),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
        };

        ClientSend.PlayerMovement(_inputs);
    }
}