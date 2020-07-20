using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class structure_script : MonoBehaviour
{
	public int maxHits;

	public int forceToHit;

	public Sprite State1;
	public Sprite State2;
	public Sprite State3;
	private int counter = 0;
	


	void Update()
    {
		SpriteRenderer[] childrenSprites = GetComponentsInChildren<SpriteRenderer>();

		if (maxHits <= counter)
		{
			Destroy(gameObject);
		}
		if (counter == 1)
		{
			for (int i = 0; i < childrenSprites.Length; i++)
			{
				childrenSprites[i].sprite = State1;
			}
			//asignar sprite un hit
		}
		if (counter == 2)
		{
			for (int i = 0; i < childrenSprites.Length; i++)
			{
				childrenSprites[i].sprite = State2;
			}
			//asignar sprite dos hit
		}
		if (counter == 3)
		{
			for (int i = 0; i < childrenSprites.Length; i++)
			{
				childrenSprites[i].sprite = State3;
			}
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
