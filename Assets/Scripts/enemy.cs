using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	public float health = 2f;
    public float enemyContact = 2f;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if( collision.relativeVelocity.magnitude > health)
		{
            enemyContact--;
            if (enemyContact<=0)
            {
                Die();
            }
            
		}

	}
	void Die()
	{
		//Here you've to animate the death of the enemy
		Destroy(gameObject);
	}
}
