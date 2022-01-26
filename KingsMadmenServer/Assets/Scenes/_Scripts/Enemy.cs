using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // ================ VARIABLES ================

    private const int DISTANCE_ATTACK = 4;

    public int id;
    public Transform currentTarget;
    public int state;
    public bool isAlreadySeen = false;
    public float health = 200;

    private NavMeshAgent agent;
    private Vector3 currentRot;
    private Quaternion currentQuaternionRot;
    private float dist;
    private bool isAttacking = false;
    private int attack;
    private DetectionHit detectHit;
    private bool isDied = false;


    // ===========================================

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        detectHit = transform.GetChild(3).GetComponent<DetectionHit>();
    }

    private void Update()
    {
        if(isDied)
        {
            return;
        }

        if (currentTarget != null)
        {
            dist = Vector3.Distance(currentTarget.position, transform.position);
        }

        if (isAlreadySeen)
        {
            if(currentTarget != null)
            {
                if(dist < DISTANCE_ATTACK)
                {
                    if(!isAttacking)
                    {
                        StartCoroutine(Attack());
                        isAttacking = true;
                    }    
                }
                else
                {
                    state = 2;
                    isAttacking = false;
                }
            }
            else
            {
                state = 1;
            }

            ServerSend.EnemyMoved(this);
            ServerSend.EnemyAnim(this);
        }

        if (currentTarget != null && !isAttacking)
        {
            agent.destination = currentTarget.position;

            //ServerSend.EnemyMoved(this);


            transform.LookAt(currentTarget);

            float y = transform.rotation.eulerAngles.y;
            float z = transform.rotation.eulerAngles.z;

            currentRot = new Vector3(0, y, z);
            currentQuaternionRot.eulerAngles = currentRot;
            transform.localRotation = currentQuaternionRot;

            ServerSend.EnemyRotation(this);
        }
    }

    IEnumerator Attack()
    {
        if(!isDied)
        {
            int choice = Random.Range(0, 6);
            if (choice == 0)
            {
                attack = 4;
            }
            else
            {
                attack = 3;
            }

            state = attack;
        }
       
        yield return new WaitForSeconds(0.6f);

        if(attack == 3 && !isDied)
        {
            if(detectHit.canHitPlayer1)
            {
                transform.GetChild(2).GetComponent<DetectionEnemy>().player1.GetComponent<Player>().TakeDamage(10);
            }

            if (detectHit.canHitPlayer2)
            {
                transform.GetChild(2).GetComponent<DetectionEnemy>().player2.GetComponent<Player>().TakeDamage(10);
            }
        }

        yield return new WaitForSeconds(0.8f);

        if (attack == 4 && !isDied)
        {
            if (detectHit.canHitPlayer1)
            {
                transform.GetChild(2).GetComponent<DetectionEnemy>().player1.GetComponent<Player>().TakeDamage(30);
            }

            if (detectHit.canHitPlayer2)
            {
                transform.GetChild(2).GetComponent<DetectionEnemy>().player2.GetComponent<Player>().TakeDamage(30);
            }
        }

        yield return new WaitForSeconds(0.1f);

        if(!isDied)
        {
            transform.LookAt(currentTarget);

            float y = transform.rotation.eulerAngles.y;
            float z = transform.rotation.eulerAngles.z;

            currentRot = new Vector3(0, y, z);
            currentQuaternionRot.eulerAngles = currentRot;
            transform.localRotation = currentQuaternionRot;
            ServerSend.EnemyRotation(this);

            isAttacking = false;
        }
        
    }

    public void TakeDamage(float _damage)
    {
        if (isDied)
        {
            return;
        }

        ServerSend.EnemyDamaged(this, _damage);

        health -= _damage;

        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
    }

    public void Die()
    {
        isDied = true;

        state = 6;
        ServerSend.EnemyAnim(this);

        StartCoroutine(DeleteEnemy());
    }

    IEnumerator DeleteEnemy()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
