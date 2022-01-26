using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClient : MonoBehaviour
{
    public int state = 0;
    public GameObject ptcHitPref;
    public GameObject ptcCriticalHitPref;
    public GameObject ptcDiePref;

    private Animator animator;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeState(int _state)
    {
        state = _state;
        animator.SetInteger("State", state);

        if(state == 6)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        GameObject.Find("ClientManager").GetComponent<SoundManager>().playAudioClip(6);

        GameObject ptcDie;
        ptcDie = Instantiate(ptcDiePref, transform.position, Quaternion.identity);
        Destroy(ptcDie, 4f);

        yield return new WaitForSeconds(1.4f);

        while(transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.02f, transform.localScale.y - 0.02f, transform.localScale.z - 0.02f);
            yield return new WaitForSeconds(0.01f);
        }

        gameObject.SetActive(false);
    }

    public void GetDamaged(float _damage)
    {
        GameObject ptcHit;

        if(_damage == 10)
        {
            ptcHit = Instantiate(ptcHitPref, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
        }
        else
        {
            ptcHit = Instantiate(ptcCriticalHitPref, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
        }

        Destroy(ptcHit, 5f);
    }
}
