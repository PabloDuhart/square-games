using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRec : MonoBehaviour
{
    public int impulseX;
    public int impulseY;
    

    private SpriteRenderer spriterender;
    private bool gravityI;
    private BoxCollider2D gravitationTrigger;


    void FixedUpdate()
    {

        foreach (var objectInVicinity in objectsInRange)
        {
            if (objectInVicinity == null)
            {
                objectsInRange.Remove(objectInVicinity);
                break;
            }

            objectInVicinity.gravityScale = 0f;
            
            objectInVicinity.AddForce(new Vector2(impulseX,impulseY));
        }


        gravitationTrigger = GetComponent<BoxCollider2D>();
        gravitationTrigger.isTrigger = true;
        
    }

    
    private List<Rigidbody2D> objectsInRange = new List<Rigidbody2D>();
    private void OnTriggerEnter2D(Collider2D collider)
    {
        var rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            objectsInRange.Add(rb);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        var rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 2.5f;
            objectsInRange.Remove(rb);

        }
    }
}
