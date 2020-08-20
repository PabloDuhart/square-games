using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathOutOfBounds : MonoBehaviour
{
 

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (!collision.collider.CompareTag("projectil"))
		{
            Destroy(collision.collider.gameObject);
        }
            
	}

}
