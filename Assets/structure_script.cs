using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class structure_script : MonoBehaviour
{
	public int maxHits;

	public int forceToHit;

	private int counter = 0;


    
    void Update()
    {
		if (maxHits <= counter)
		{
			Destroy(gameObject);
		}
		if (counter == 1)
		{
			//asignar sprite un hit
		}
		if (counter == 2)
		{
			//asignar sprite dos hit
		}
		if (counter == 3)
		{
			//asignar sprite tres hit
		}
	
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.CompareTag("projectil"))
		{
			if (collision.relativeVelocity.magnitude > forceToHit)
			{
				counter += 1;
			}
		}
	}





}
