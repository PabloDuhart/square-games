using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class structure_script : MonoBehaviour
{
    [Range(1,2)] public int maxHits;

	public int forceToHit;

	public Sprite State1;
	public Sprite State2;
	public Sprite State3;
	public Sprite Kill1;
	public Sprite Kill2;
	public Sprite Kill3;
	private int counter = 0;
	private bool kill = false; 


    private void Start()
    {
        if (maxHits>2){
            maxHits = 2;
        }
    }
    void Update()
    {
		SpriteRenderer[] childrenSprites = GetComponentsInChildren<SpriteRenderer>();

		if (maxHits <= counter && !kill)
		{
			kill = true;
			StartCoroutine(Kill(childrenSprites));
		}
		if (counter == 1 && !kill)
		{
			for (int i = 0; i < childrenSprites.Length; i++)
			{
				childrenSprites[i].sprite = State1;
			}
			//asignar sprite un hit
		}
		if (counter == 2 && !kill)
		{
			for (int i = 0; i < childrenSprites.Length; i++)
			{
				childrenSprites[i].sprite = State2;
			}
			//asignar sprite dos hit
		}
		if (counter == 3 && !kill)
		{
			for (int i = 0; i < childrenSprites.Length; i++)
			{
				childrenSprites[i].sprite = State3;
			}
			//asignar sprite tres hit
		}



	
	}


	IEnumerator Kill(SpriteRenderer [] childrenSprites)
	{
		yield return new WaitForSeconds(0.2f);
		for (int i = 0; i < childrenSprites.Length; i++)
		{
			childrenSprites[i].sprite = Kill1;
		}
		yield return new WaitForSeconds(0.2f);
		for (int i = 0; i < childrenSprites.Length; i++)
		{
			childrenSprites[i].sprite = Kill2;
		}
		yield return new WaitForSeconds(0.2f);
		for (int i = 0; i < childrenSprites.Length; i++)
		{

			childrenSprites[i].sprite = Kill3;
		}
		Destroy(gameObject);
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
