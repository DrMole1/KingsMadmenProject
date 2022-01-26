using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public CharacterController controller;
    public Transform shootOrigin;
    public float gravity = -9.81f;
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public float health;
    public float maxHealth = 100f;
    public float resist = 1f;
    public bool isBoosted = false;
    public bool isShielding = false;
    public bool isBackOnEnemy = false;

    private bool[] inputs;
    private float yVelocity = 0;
    private bool canMoveAnim = true;
    private bool isTeleporting = false;
    private bool hasGlyphe = false;
    private Vector3 posGlyphe;

    private void Start()
    {
        gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        moveSpeed *= Time.fixedDeltaTime;
        jumpSpeed *= Time.fixedDeltaTime;
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;

        inputs = new bool[5];
    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        if (health <= 0f)
        {
            return;
        }

        Vector2 _inputDirection = Vector2.zero;
        if (inputs[0])
        {
            _inputDirection.y += 1;
        }
        if (inputs[1])
        {
            _inputDirection.y -= 1;
        }
        if (inputs[2])
        {
            _inputDirection.x -= 1;
        }
        if (inputs[3])
        {
            _inputDirection.x += 1;
        }

        Move(_inputDirection);
    }

    /// <summary>Calculates the player's desired movement direction and moves him.</summary>
    /// <param name="_inputDirection"></param>
    private void Move(Vector2 _inputDirection)
    {
        if(isTeleporting)
        {
            return;
        }


        Vector3 _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        _moveDirection *= moveSpeed;

        ServerSend.PlayerAnimRun(this, _moveDirection, canMoveAnim);

        if (controller.isGrounded)
        {
            yVelocity = 0f;
            if (inputs[4])
            {
                yVelocity = jumpSpeed;
            }
        }

        yVelocity += gravity;

        _moveDirection.y = yVelocity;
        controller.Move(_moveDirection);

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    /// <summary>Updates the player input with newly received input.</summary>
    /// <param name="_inputs">The new key inputs.</param>
    /// <param name="_rotation">The new rotation.</param>
    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        transform.rotation = _rotation;
    }

    public void Shoot(Vector3 _viewDirection)
    {
        if (Physics.Raycast(shootOrigin.position, _viewDirection, out RaycastHit _hit, 4f))
        {
            if (_hit.collider.CompareTag("Enemy"))
            {
                if(!isBoosted)
                {
                    _hit.collider.GetComponent<Enemy>().TakeDamage(10f);

                    if(isBackOnEnemy)
                    {
                        Regenerate(5);
                    }
                }
                else
                {
                    _hit.collider.GetComponent<Enemy>().TakeDamage(30f);
                    isBoosted = false;

                    if (isBackOnEnemy)
                    {
                        Regenerate(15);
                    }
                }
            }
        }
    }

    public void TakeDamage(float _damage)
    {
        if (health <= 0f)
        {
            return;
        }

        health -= _damage * resist;

        if (health <= 0f)
        {
            health = 0f;
            controller.enabled = false;
            transform.position = new Vector3(0f, 25f, 0f);
            ServerSend.PlayerPosition(this);
            //StartCoroutine(Respawn());
        }

        ServerSend.PlayerHealth(this);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);

        health = maxHealth;
        controller.enabled = true;
        ServerSend.PlayerRespawned(this);
    }

    public void Shield(bool _isShielding)
    {
        isShielding = _isShielding;

        if(_isShielding)
        {
            moveSpeed = 1f * Time.fixedDeltaTime;
            jumpSpeed = 6f * Time.fixedDeltaTime;
            resist = 0.5f;
        }
        else
        {
            moveSpeed = 5f * Time.fixedDeltaTime;
            jumpSpeed = 9f * Time.fixedDeltaTime;
            resist = 1f;
        }
    }

    public void Teleportation()
    {
        if(hasGlyphe)
        {
            hasGlyphe = false;
            isTeleporting = true;
            resist = 0;
            Debug.Log("startTeleportation");

            StartCoroutine(TeleportationTraveling());
        }
    }

    public void PutGlyphe()
    {
        if(controller.isGrounded)
        {
            hasGlyphe = true;
            posGlyphe = transform.position;

            Debug.Log("put glyphe");
        }
    }

    IEnumerator TeleportationTraveling()
    {
        yield return new WaitForSeconds(0.02f);

        transform.position = posGlyphe;

        yield return new WaitForSeconds(0.02f);

        isTeleporting = false;
        resist = 1;
        float buffSpeed = 15f;
        moveSpeed = buffSpeed * Time.fixedDeltaTime;
        isBoosted = true;

        while (buffSpeed > 5f && !isShielding)
        {
            buffSpeed -= 1f;

            yield return new WaitForSeconds(0.2f);

            moveSpeed = buffSpeed * Time.fixedDeltaTime;
        }

        isBoosted = false;
    }

    public void Regenerate(int _healing)
    {
        if (health >= health + _healing)
        {
            return;
        }

        health += _healing;

        ServerSend.PlayerHealth(this);
    }
}