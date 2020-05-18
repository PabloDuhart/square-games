using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_Script : MonoBehaviour
{

	private bool aiming = false;

	private bool launching = false;

	public Rigidbody2D rigidBody;

	public Rigidbody2D projectileBase;

	public SpringJoint2D springJoint;

	public GameObject nextBall;

	public float launchDelay = 0.20f;

	public float aimLimit = 4f;

	public float nextBallDelay = 2f;



	void Update()
    {

        if (aiming)
		{
			Vector2 projectilePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if ( Vector3.Distance(projectilePosition, projectileBase.position) > aimLimit)
			{
				rigidBody.position = projectileBase.position + (projectilePosition - projectileBase.position).normalized * aimLimit;
			}
			else
			{
				rigidBody.position = projectilePosition;
			}
		}
    }


	void OnMouseDown()
	{
		if (!launching)
		{
			aiming = true;
			rigidBody.isKinematic = true;
		}
		
	}

	void OnMouseUp()
	{
		aiming = false;
		rigidBody.isKinematic = false;
		StartCoroutine(Launch());		
	}

	IEnumerator Launch()
	{
		yield return new WaitForSeconds(launchDelay);
		springJoint.enabled = false;
		launching = true;
		yield return new WaitForSeconds(nextBallDelay);
		if(nextBall == null)
		{
			//Here the player losses because he/she doesn't have any ammo left, this part of the script is subceptible to changes because of the way it handdles the ammo
			//For now this is how we are going to test the game, if the reader has any other better way of implementing this action please inform to the rest of the team
		}
		else
		{
			gameObject.SetActive(false);
			nextBall.SetActive(true);
		}
		
	}



}
