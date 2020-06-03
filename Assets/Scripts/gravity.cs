using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gravity : MonoBehaviour
{
    [Header("Position of 'Black Hole'")]
    public Vector2 whereCanSpawnX;
    public Vector2 whereCanSpawnY;
    [Header("Gravity variables")]
    public int gravitationMin;//valor recomendado: 20
    public int gravitationMax;//valor recomendado: 50
    [Header("Gravity radio")]
    public int gravitationRadiusMin;//valor recomendado: 4
    public int gravitationRadiusMax;//valor recomendado: 10
    [Header("Gravity rotation")]
    public int rotationSpeedMin;//valor recomendado: 1
    public int rotationSpeedMax;//valor recomendado: 10
    //private int rotationSpeed;
    private int gravitationRadius;
    private int gravitation;
    private int randompositionX;
    private int randompositionY;

    private CircleCollider2D gravitationTrigger;

    void Start()
    {
        randompositionX = new System.Random().Next((int)whereCanSpawnX.x, (int)whereCanSpawnY.x);//posición aleatoria dentro del rango establecido en la escena
        randompositionY = new System.Random().Next((int)whereCanSpawnX.y, (int)whereCanSpawnY.y);//posición aleatoria dentro del rango establecido en la escena
        Debug.Log(randompositionX);
        Debug.Log(randompositionY);
		Color childrenColor = GetComponentInChildren<SpriteRenderer>().color;
		while (childrenColor.a < 1)
		{
			childrenColor.a += 0.05f;
			GetComponentInChildren<SpriteRenderer>().color = childrenColor;
		}
		gameObject.transform.position = new Vector2(randompositionX,randompositionY);
        gravitation = new System.Random().Next(gravitationMin, gravitationMax);
        gravitationRadius = new System.Random().Next(gravitationRadiusMin,gravitationRadiusMax);
        //rotationSpeed = new System.Random().Next(rotationSpeedMin,rotationSpeedMax);
        gravitationTrigger = GetComponent<CircleCollider2D>();
        gravitationTrigger.isTrigger = true;
        gravitationTrigger.radius = gravitationRadius / transform.localScale.x;
        StartCoroutine(Wait());
    }

    void FixedUpdate()
    {
        
        foreach (var objectInVicinity in objectsInRange)
        {
            if (objectInVicinity == null)
            {
                objectsInRange.Remove(objectInVicinity);
                break;
            }
            
            int random = new System.Random().Next(0, 5);
            float dist = Vector2.Distance(transform.position, objectInVicinity.transform.position);
            float gravitationFactor = -1 + dist; //para que se atraiga es el -1
            Vector2 force = (transform.position - objectInVicinity.transform.position).normalized * gravitation * gravitationFactor*random;
            objectInVicinity.AddForce(force);
        }
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
            objectsInRange.Remove(rb);
        }
    }




    private IEnumerator Wait()
    {
		yield return new WaitForSeconds(10);
		gravitation = 0;
		gravitationRadius = 0;
		Color childrenColor = GetComponentInChildren<SpriteRenderer>().color;
		
		while (childrenColor.a > 0)
		{
			yield return new WaitForSeconds(0.05f);
			childrenColor.a += -0.05f;
			GetComponentInChildren<SpriteRenderer>().color = childrenColor;
		}
        int randomtime = new System.Random().Next(100,150);//tiempo aleatorio en el que puede reaparecer un campo gravitacional entre 4 sc y 10 sc
        yield return new WaitForSeconds(randomtime);
        Instantiate(gameObject);
        Destroy(gameObject);
    }
}

