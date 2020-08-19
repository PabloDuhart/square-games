using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float pushForce;
    private List<Rigidbody2D> objectsInRange;
    protected bool readyToTransparentChilds;
    public void Start()
    {
        objectsInRange = new List<Rigidbody2D>();
    }
    public void Update()
    {
        if (objectsInRange.Count==0){
            readyToTransparentChilds = true;
        }
        else
        {
            readyToTransparentChilds = false;
            foreach (var objectInVicinity in objectsInRange)
            {
                Vector2 velocity = objectInVicinity.velocity;
                float x = velocity.x;
                float y = velocity.y;
                objectInVicinity.AddForce(new Vector2(-x * pushForce, pushForce));

            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null && !(collision.gameObject.CompareTag("projectil")))
        {
            StartCoroutine(shieldActiveTime());
            objectsInRange.Add(rb);
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject spaceshipshield = gameObject.transform.GetChild(i).gameObject;
                spaceshipshield.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        var rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null && !(collider.gameObject.CompareTag("projectil")))
        {
            objectsInRange.Remove(rb);
        }
    }
    private IEnumerator shieldActiveTime()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject spaceshipshield = gameObject.transform.GetChild(i).gameObject;
            spaceshipshield.SetActive(false);
        }
    }

    public bool getReadyToTransparentChild()
    {
        return readyToTransparentChilds;
    }
}
