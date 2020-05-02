using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_Script : MonoBehaviour
{

	private bool aiming = false;

	public Rigidbody2D rigidBody;

	public SpringJoint2D springJoint;

	public double distanceOffset = 0;
   
    void Update()
    {
        if (aiming)
		{
			rigidBody.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
    }


	void OnMouseDown()
	{
		aiming = true;
		rigidBody.isKinematic = true;
	}

	void OnMouseUp()
	{
		aiming = false;
		rigidBody.isKinematic = false;
		if (springJoint.distance > distanceOffset)
		{
			springJoint.enabled = false;
		}
	}
}
