using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemyContact = 2f; //enemy lifes
	private void OnCollisionEnter2D(Collision2D collision)
	{
	    if (collision.collider.CompareTag("projectil")){
            //Enemy damaged, here you can put the animation.
            enemyContact--;
        }
        else
        {
            //Enemy damaged, here you can put the animation.
            enemyContact -= 0.5f;
        }
        if (enemyContact <= 0)
        {
            //Enemy die, here you can put the animation.
            Destroy(gameObject);
        }
    }
}
