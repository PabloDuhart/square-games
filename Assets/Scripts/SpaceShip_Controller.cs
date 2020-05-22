using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_Controller : MonoBehaviour
{
    
    public Vector3 start;
    public Vector3 preLanding;
    public Vector3 ground;
    public float startDelay;
	public float velocity;
	public Vector3 spaceShipScale;
	public projectile_Script proyectil;
	public GameObject canvasInterface;

	private Animator anim;
	private float distance;
    private bool preLand;
    private bool landing;
    private float groundPositionY;
    private bool toSpace;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);//Change scale of spaceShip
        gameObject.transform.rotation = new Quaternion(0f, 0f, -45, 110);//Change rotation of spaceShip
        gameObject.transform.localPosition = start;//Change position of spaceShip
        anim.SetBool("Grounded", false);
        anim.SetBool("FirstReload", false);
        anim.SetBool("Idle", false);
        preLand = false;
        distance = 0;
        anim.SetBool("Flying", true);
        toSpace = true;
        anim.SetBool("Reload", false);
        anim.SetBool("Attack", false);
        groundPositionY = ground.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (toSpace)
        {
            if (gameObject.transform.localPosition.Equals(preLanding))
            {
                preLand = true;
                gameObject.transform.localScale = spaceShipScale;
                gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                distance = velocity;
                toSpace = false;
            }
            else
            {
                gameObject.transform.localPosition = Vector3.MoveTowards(start, preLanding, distance);
                distance += velocity;
            }
        }
        if (preLand)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(preLanding, ground, distance);
            distance += velocity;
        }
        if ((Mathf.Abs(gameObject.transform.position.y-groundPositionY))<1f & (Mathf.Abs(gameObject.transform.position.y - groundPositionY))!=0 & !landing)
        {//if the distance between the spaceship and the ground is less than 1 and !=0 the object is landing
            preLand = false;
            landing = true;
            anim.SetBool("Flying", false);
            gameObject.transform.localPosition = Vector3.MoveTowards(preLanding, ground, distance);
            distance += velocity;
            StartCoroutine(WaitLanding());
        }
        if (landing)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(preLanding, ground, distance);
            distance += velocity;
        }
        if ((Mathf.Abs(gameObject.transform.position.y - groundPositionY)) == 0 && !anim.GetBool("FirstReload"))
        {
            //anim.SetBool("Grounded", true);
            StartCoroutine(Wait());
        }
        //if (proyectil.getLaunchAnimation())
        //{
        //    anim.SetBool("Attack", true);
         //   StartCoroutine(WaitAttack());
        //}
        
    }
    IEnumerator WaitLanding()
    {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Grounded", true);
		canvasInterface.SetActive(true);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6.05f);
        anim.SetBool("FirstReload", true);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Idle", true);
    }
    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("Attack", false);
        anim.SetBool("Reload", true);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Reload", false);
    }
}
