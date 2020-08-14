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

    public Text BallText;

    public GameObject gameOverCanvas;
    public GameObject youWinCanvas;

    private GameObject audioManager;

    public float addForce;
    
    public float NextBalltime;

    //public GameObject zoom;

    private bool ballOnCup;

    private bool mouseClicks = false;

    public float publicValue;

    public int level;

    private AudioSource tik;

    private int numberOfTiks = 10;

	void Awake()
	{
        tik = gameObject.GetComponent<AudioSource>();
        audioManager = GameObject.Find("AudioManager");
    }


	void Update()
    {
       
        
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


        if (playerLifes <= 0 && !ballOnCup)//if projectiles left its 0 and the scene have more enemys, the player lose.
        {
            audioManager.SetActive(false);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);
        }
        
        if (playerLifes >= 0 && ballOnCup)
        {
            gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("levelReached") <= level)
			{
                PlayerPrefs.SetInt("levelReached", level + 1);
                PlayerPrefs.Save();
            }
         
            Time.timeScale = 0f;
            audioManager.SetActive(false);
            youWinCanvas.SetActive(true);
            
        }
        
    }


    void OnMouseDown()
    {
        if (!launching && mouseClicks == false)
        {
            mouseClicks = true;
            aiming = true;
            rigidBody.isKinematic = true;
        }


    }


    void OnMouseUp()
    {
        if (mouseClicks)
        {
            aiming = false;
            rigidBody.isKinematic = false;
            ColliderCircular.radius = 2.5f;
            launching = true;
            mouseClicks = false;
            BallText.text = "Balls left: " + (playerLifes - 1).ToString();
            StartCoroutine(Launch());
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cup"))
        {
            ballOnCup = true;
        }
  
        if (collision.relativeVelocity.magnitude > 5f)
		{
            tik.Play();
            numberOfTiks = 10;
		}
		if (collision.relativeVelocity.magnitude <= 5f && numberOfTiks > 0)
		{
            numberOfTiks--;
            tik.Play();
        }
            
		
        
    }
    IEnumerator Launch()
    {
        yield return new WaitForSeconds(launchDelay);
        springJoint.enabled = false;
        
        rigidBody.velocity *= addForce;
        StartCoroutine(ballToBall(NextBalltime));
    }
    private IEnumerator ballToBall(float time)
    {
        ColliderCircular.radius = publicValue;
        yield return new WaitForSeconds(time);
		if (playerLifes - 1 > 0)
		{
            nextBallCode.rigidBody.isKinematic = true;
            nextBallCode.springJoint.enabled = true;
            nextBallCode.rigidBody.isKinematic = false;
            nextBall.SetActive(true);
        
            //Projectil respawn code
            aiming = false;
            playerLifes--;
            nextBallCode.playerLifes = playerLifes;
		}
		else
		{
            playerLifes--;
		}

      
    }
}