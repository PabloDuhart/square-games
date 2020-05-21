using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_Script : MonoBehaviour
{

	private bool aiming = false;

	protected bool launching = false;

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
        if (playerLifes <= 0 & enemys.Length>0)//if projectiles left its 0 and the scene have more enemys, the player lose.
        {
            gameObject.SetActive(false);
            Debug.Log("YOU LOSE AJAJAJAJAJAJ");
            //Here the player losses because he/she doesn't have any ammo left
            //Call dead menu
        }
        if (enemys.Length==0 & playerLifes >= 0)
        {
            gameObject.SetActive(false);
            Debug.Log("You WIN THE GAME");
            //Here the player win the game
            //Call win menu or canvas. 
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
		yield return new WaitForSeconds(nextProjectileDelay);
        //Projectil respawn code
		nextProjectile.SetActive(true);
        nextProjectile.transform.position = projectilePosition;
        gameObject.SetActive(false);
        springJoint.enabled = true;
        aiming = false;
        launching = false;
        playerLifes--;
        nextProjectilCode.playerLifes--;
        Debug.Log("Player lifes: "+playerLifes);//Detete this before a sprint
        Debug.Log("Enemy count: "+enemys.Length);//Detete this before a sprint
    }
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.transform.position = new Vector2(-1111111, -111111111);//Tp the object far away jejejejejejej
        yield return new WaitForSeconds(nextProjectileDelay);
        //Projectil respawn code
        nextProjectile.SetActive(true);
        nextProjectile.transform.position = projectilePosition;
        gameObject.SetActive(false);
        springJoint.enabled = true;
        aiming = false;
        launching = false;
        playerLifes--;
        nextProjectilCode.playerLifes--;
        Debug.Log("Player lifes: " + playerLifes);//Delete this before a sprint
        Debug.Log("Enemy count: " + enemys.Length);//Delete this before a sprint
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
