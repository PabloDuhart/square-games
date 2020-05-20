using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Animator anim;
    public float enemyContact = 2f;//enemy lifes
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("EnemyLife", enemyContact);
    }
   
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
	{
	    if (collision.collider.CompareTag("projectil")){
            //Enemy damaged, here you can put the animation.
            enemyContact--;
            anim.SetBool("HitDamage",true);
            anim.SetFloat("EnemyLife", enemyContact);
        }
        else
        {
            //Enemy damaged, here you can put the animation.
            enemyContact -= 0.5f;
            anim.SetBool("HitDamage", true);
            anim.SetFloat("EnemyLife", enemyContact);
        }
        if (enemyContact <= 0)
        {
            //Enemy die, here you can put the animation.
            anim.SetFloat("EnemyLife", enemyContact);
            yield return new WaitForSeconds(0.58f);
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil"))
        {
            //Enemy damaged, here you can put the animation.
         
            anim.SetBool("HitDamage", false);
            
        }
    }
}
