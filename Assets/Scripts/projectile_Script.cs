﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projectile_Script : MonoBehaviour
{

	private bool aiming = false;

	protected bool launching = false;

    public CircleCollider2D ColliderCircular;

	public Rigidbody2D rigidBody;

	public Rigidbody2D projectileBase;

	public SpringJoint2D springJoint;

	public GameObject nextProjectile;

	public float launchDelay = 0.20f;

	public float aimLimit = 4f;

	public float nextProjectileDelay = 2f;

	public int playerLifes;//Projectiles left.

	public projectile_Script nextProjectilCode;//The "projectile_Script" of the next projectil, we need this for change vars

	private GameObject[] enemys;//list of enemys 

	public Vector3 projectilePosition;

	public Text projectilesText;
	public Text enemiesText;

	public GameObject gameOverCanvas;
	public GameObject youWinCanvas;
    public float addForce;
    public float forceToDestroy;

	public float fadingDelay;

    void Update()
    {


        enemys = GameObject.FindGameObjectsWithTag("enemy");
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


		projectilesText.text = "Projectiles left: " + playerLifes.ToString();
		enemiesText.text = "Enemies left: " + (enemys.Length).ToString();



		if (playerLifes <= 0 & enemys.Length>0)//if projectiles left its 0 and the scene have more enemys, the player lose.
        {
            gameObject.SetActive(false);
			Time.timeScale = 0f;
			gameOverCanvas.SetActive(true);
            
        }
        if (enemys.Length==0 & playerLifes >= 0)
        {
            gameObject.SetActive(false);
			Time.timeScale = 0f;
			youWinCanvas.SetActive(true);
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
        ColliderCircular.radius = 0.25f;
		StartCoroutine(Launch());
	}

	IEnumerator Launch()
	{
        
        yield return new WaitForSeconds(launchDelay);
		springJoint.enabled = false;
		launching = true;
        rigidBody.velocity *= addForce;


    }
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("EnemyStructure"))// falta ponerle el tag a cada uno de los tiles, nose como se hace eso uwu
        {
            Debug.Log(collision.relativeVelocity.magnitude);
            if (collision.relativeVelocity.magnitude > forceToDestroy)
            {
                Destroy(collision.gameObject);
            }
        }

		yield return new WaitForSeconds(fadingDelay);
		gameObject.transform.position = new Vector2(-18.69f, 5.78f);//Tp far away
        ColliderCircular.radius = 0.9f;
		yield return new WaitForSeconds(nextProjectileDelay);

		
		
        //Projectil respawn code
		nextProjectile.transform.position = projectilePosition;
		nextProjectilCode.rigidBody.isKinematic = true;
		nextProjectilCode.springJoint.enabled = true;
		yield return new WaitForSeconds(1.5f);
		nextProjectilCode.rigidBody.isKinematic = false;
		nextProjectile.SetActive(true);

		gameObject.SetActive(false);



		aiming = false;
        launching = false;
        playerLifes--;
        nextProjectilCode.playerLifes--;
    }
    private bool getLaunching()
    {
        return launching;
    }
    public bool getLaunchAnimation()
    {
        if (launching || nextProjectilCode.getLaunching())
        {
            return true;
        }
        return false;
    }



}
