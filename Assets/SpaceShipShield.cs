using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShield : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Gratity Spawn Times")]
    public float gravitationRadius;
    public int gravitation;

    private SpriteRenderer spriterender;
    private bool gravityI;
    private CircleCollider2D gravitationTrigger;




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
            float dist = Vector2.Distance(transform.position, objectInVicinity.transform.position);
            float gravitationFactor = -1 + dist; //for attraction change -1 to 1
            Vector2 force = (transform.position - objectInVicinity.transform.position).normalized * gravitation * gravitationFactor;
            objectInVicinity.AddForce(force);
        }


        gravitationTrigger = GetComponent<CircleCollider2D>();
        gravitationTrigger.isTrigger = true;
        gravitationTrigger.radius = gravitationRadius / transform.localScale.x;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gravitationRadius);
    }

    private List<Rigidbody2D> objectsInRange = new List<Rigidbody2D>();
    private void OnTriggerEnter2D(Collider2D collider)
    {
        var rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null && !collider.gameObject.CompareTag("projectil"))
        {
            objectsInRange.Add(rb);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        var rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null && !collider.gameObject.CompareTag("projectil"))
        {
            rb.gravityScale = 2.5f;
            objectsInRange.Remove(rb);

        }
    }
}
