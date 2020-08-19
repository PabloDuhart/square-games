using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private Animator anim;
    public float enemyContact = 3f;//enemy lifes
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("EnemyLife2", enemyContact);
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil"))
        {
            //Enemy damaged, here you can put the animation.
            enemyContact--;
            anim.SetBool("HitDamage2", true);
            anim.SetFloat("EnemyLife2", enemyContact);
        }
        if (collision.collider.CompareTag("EnemyStructure") && collision.relativeVelocity.magnitude > 1f)
        {
            enemyContact -= 0.5f;
            anim.SetBool("HitDamage2", true);
            anim.SetFloat("EnemyLife2", enemyContact);
            StartCoroutine(dmgwait2());
        }

        if (enemyContact < 0.1f)
        {
            //Enemy die, here you can put the animation.
            anim.SetFloat("EnemyLife2", enemyContact);
            yield return new WaitForSeconds(1.2f);
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil") || collision.collider.CompareTag("EnemyStructure"))
        {
            //Enemy damaged, here you can put the animation.

            anim.SetBool("HitDamage2", false);

        }
    }

    private IEnumerator dmgwait2()
    {
        yield return new WaitForSeconds(0.31f);
        if (anim.GetBool("HitDamage2"))
        {
            anim.SetBool("HitDamage2", false);
        }
    }
}
