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
    public int gravitationMin;//recommended value: 20
	public int gravitationMax;//recommended value: 50
	[Header("Gravity radio")]
    public int gravitationRadiusMin;//recommended value: 4
	public int gravitationRadiusMax;//recommended value: 10
	[Header("Gravity rotation")]
    public int rotationSpeedMin;//recommended value: 1
	public int rotationSpeedMax;//recommended value: 10

	[Header("Gratity Spawn Times")]
	public int minFirstSpawn;
	public int maxFirstSpawn;
	public int minGravitationalLifespan;
	public int maxGravitationalLifespan;
	public int minNextGravitationalPull;
	public int maxNextGravitationalPull;

	

	//private int rotationSpeed;
	private int gravitationRadius;
    private int gravitation;
    private int randompositionX;
    private int randompositionY;

    //private Color childrenColor;

    private bool gravityI;

    private CircleCollider2D gravitationTrigger;

    void Start()
    {
        randompositionX = new System.Random().Next((int)whereCanSpawnX.x, (int)whereCanSpawnX.y);//random position X in scene 
        randompositionY = new System.Random().Next((int)whereCanSpawnY.x, (int)whereCanSpawnY.y);//random position Y in scene
        gameObject.transform.position = new Vector2(randompositionX,randompositionY);
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
            float gravitationFactor = -1 + dist; //for attraction change -1 to 1
            Vector2 force = (transform.position - objectInVicinity.transform.position).normalized * gravitation * gravitationFactor*random;
            objectInVicinity.AddForce(force);
        }

		Color childrenColor = GetComponentInChildren<SpriteRenderer>().color;

		if (gravityI)
        {
            childrenColor.a += 0.01f;
            GetComponentInChildren<SpriteRenderer>().color = childrenColor;
            if (childrenColor.a >= 1)
            {
                gravityI = false;
            }
            if(childrenColor.a >= 0.5f)
            {
                gravitation = new System.Random().Next(gravitationMin, gravitationMax);
                gravitationRadius = new System.Random().Next(gravitationRadiusMin, gravitationRadiusMax);
                //rotationSpeed = new System.Random().Next(rotationSpeedMin,rotationSpeedMax);
                gravitationTrigger = GetComponent<CircleCollider2D>();
                gravitationTrigger.isTrigger = true;
                gravitationTrigger.radius = gravitationRadius / transform.localScale.x;
            }
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
		int randomFirst = new System.Random().Next(minFirstSpawn, maxFirstSpawn);
		yield return new WaitForSeconds(randomFirst);
		gravityI = true;
		int randomLifespan = new System.Random().Next(minGravitationalLifespan, maxGravitationalLifespan);
		yield return new WaitForSeconds(randomLifespan);
		gravitation = 0;
		gravitationRadius = 0;
		Color childrenColor = GetComponentInChildren<SpriteRenderer>().color;
		while (childrenColor.a > 0)
		{
			yield return new WaitForSeconds(0.05f);
			childrenColor.a += -0.05f;
			GetComponentInChildren<SpriteRenderer>().color = childrenColor;
		}
        int randomtime = new System.Random().Next(minNextGravitationalPull,maxNextGravitationalPull);//random type in which the gravitational pull can reappear
        yield return new WaitForSeconds(randomtime);
        Instantiate(gameObject);
        Destroy(gameObject);
    }
}

