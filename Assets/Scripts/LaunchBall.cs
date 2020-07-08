using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchBall : MonoBehaviour
{
    private bool aiming = false;

    protected bool launching = false;

    public CircleCollider2D ColliderCircular;

    public Rigidbody2D rigidBody;

    public Rigidbody2D BallBase;

    public SpringJoint2D springJoint;

    public GameObject nextBall;

    public float launchDelay = 0.20f;

    public float aimLimit = 4f;

    public float nextBallDelay = 2f;

    public int playerLifes;//Projectiles left.

    public LaunchBall nextBallCode;//The "projectile_Script" of the next projectil, we need this for change vars
    
    public Vector3 BallPosition;

    //public Text BallText;

    public GameObject gameOverCanvas;
    public GameObject youWinCanvas;
    public float addForce;
    
    public float NextBalltime;

    //public GameObject zoom;

    private bool ballOnCup;

    private bool onBall = true;

    private bool derrota = false;

    private void Start()
    {
        if (NextBalltime==10f)
        {
            derrota = true;
        }
    }
    void Update()
    {
        if(rigidBody.velocity.magnitude < 0.001f && onBall)
        {
            onBall = false;
            StartCoroutine(ballToBall(1f,derrota));
        }
        
        if (aiming)
        {
            Vector2 projectilePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(projectilePosition, BallBase.position) > aimLimit)
            {
                rigidBody.position = BallBase.position + (projectilePosition - BallBase.position).normalized * aimLimit;
            }
            else
            {
                rigidBody.position = projectilePosition;
            }
        }


        //BallText.text = "Projectiles left: " + playerLifes.ToString();




        if (playerLifes <= 0 && !ballOnCup)//if projectiles left its 0 and the scene have more enemys, the player lose.
        {
            derrota = true;
            gameObject.SetActive(false);
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);

        }
        if (playerLifes >= 0 && ballOnCup)
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
            //zoom.SetActive(true);
        }

    }

    void OnMouseUp()
    {
        aiming = false;
        rigidBody.isKinematic = false;
        ColliderCircular.radius = 2.5f;
        //zoom.SetActive(false);
        StartCoroutine(Launch());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("glass"))
        {
            ballOnCup = true;
        }
    }
    IEnumerator Launch()
    {

        yield return new WaitForSeconds(launchDelay);
        springJoint.enabled = false;
        launching = true;
        rigidBody.velocity *= addForce;
        StartCoroutine(ballToBall(NextBalltime,derrota));

    }
    private IEnumerator ballToBall(float time,bool condition)
    {
        if (condition)
        {
            ColliderCircular.radius = 3f;
            yield return new WaitForSeconds(time);
            //Projectil respawn code
            nextBallCode.rigidBody.isKinematic = true;
            nextBallCode.springJoint.enabled = true;
            nextBallCode.rigidBody.isKinematic = false;
            nextBall.SetActive(true);
            aiming = false;
            launching = false;
            playerLifes--;
            nextBallCode.playerLifes--;
        }
    }
}