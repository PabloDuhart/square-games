using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    private Animator anim;
    public float enemyContact = 7f;//enemy lifes
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("EnemyLife3", enemyContact);
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil"))
        {
            //Enemy damaged, here you can put the animation.
            enemyContact--;
            anim.SetBool("HitDamage3", true);
            anim.SetFloat("EnemyLife3", enemyContact);
        }
        
        if (enemyContact < 0.1f)
        {
            //Enemy die, here you can put the animation.
            anim.SetFloat("EnemyLife3", enemyContact);
            yield return new WaitForSeconds(1.2f);
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil"))
        {
            //Enemy damaged, here you can put the animation.

            anim.SetBool("HitDamage3", false);

        }
    }
}
