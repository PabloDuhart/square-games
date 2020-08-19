using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projectile_Script : MonoBehaviour
{

    private bool aiming = false;

    protected bool launching = false;

    public CircleCollider2D ColliderCircular;

    private Animator anim;

    public Rigidbody2D rigidBody;

    public Rigidbody2D projectileBase;

    public SpringJoint2D springJoint;

    public GameObject nextProjectile;

    public float launchDelay = 0.20f;

    public float aimLimit = 4f;

    public int playerLifes;//Projectiles left.

    public projectile_Script nextProjectilCode;//The "projectile_Script" of the next projectil, we need this for change vars

    private GameObject[] enemys;//list of enemys 

    public Vector3 projectilePosition;

    public Vector2 tpAway;

    public Text projectilesText;
    public Text enemiesText;

    public GameObject gameOverCanvas;
    public GameObject youWinCanvas;
    public float addForce;
    public float forceToDestroy;

    public float fadingDelay;
    public GameObject launcheffect;
    public GameObject impacteffect;
    private bool impactEffect = true;
    private bool launchEffect = true;
    public Vector3 actualposition;
    private GameObject audioManager;


    public GameObject zoom;
    public int level;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager");
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        actualposition = gameObject.transform.position;
        enemys = GameObject.FindGameObjectsWithTag("enemy");
        if (aiming)
        {
            Vector2 projectilePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(projectilePosition, projectileBase.position) > aimLimit)
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



        if (playerLifes <= 0 & enemys.Length > 0)//if projectiles left its 0 and the scene have more enemys, the player lose.
        {
            audioManager.SetActive(false);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);

        }
        if (enemys.Length == 0 & playerLifes >= 0)
        {
            if (PlayerPrefs.GetInt("levelReached2") <= level)
            {
                PlayerPrefs.SetInt("levelReached2", level + 1);
                PlayerPrefs.Save();
            }
            gameObject.SetActive(false);
            Time.timeScale = 0f;
            audioManager.SetActive(false);
            youWinCanvas.SetActive(true);
        }
    }


    void OnMouseDown()
    {
        if (!launching)
        {
            aiming = true;
            rigidBody.isKinematic = true;
            zoom.SetActive(true);
        }

    }

    void OnMouseUp()
    {
        aiming = false;
        rigidBody.isKinematic = false;
        ColliderCircular.radius = 0.25f;
        zoom.SetActive(false);
        if (launchEffect)
        {
            launchEffect = false;
            Instantiate(launcheffect);
        }
        StartCoroutine(Launch());
    }

    IEnumerator Launch()
    {

        yield return new WaitForSeconds(launchDelay);
        springJoint.enabled = false;
        launching = true;
        rigidBody.velocity *= addForce;
    }
    public IEnumerator Death()
    {
        if (impactEffect)
        {
            impactEffect = false;
            Instantiate(impacteffect);
        }
        yield return new WaitForSeconds(0.39f);
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(0.5f);
        rigidBody.isKinematic = true;
        springJoint.enabled = false;
        gameObject.transform.position = tpAway;//Tp far away
        anim.SetBool("Death", false);
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        ColliderCircular.radius = 0.9f;
        yield return new WaitForSeconds(1.5f);

        //Projectil respawn code
        nextProjectilCode.rigidBody.isKinematic = true;
        nextProjectilCode.springJoint.enabled = false;
        nextProjectilCode.rigidBody.constraints = RigidbodyConstraints2D.None;
        nextProjectile.transform.position = projectilePosition;

        nextProjectilCode.impactEffect = true;
        nextProjectilCode.launchEffect = true;
        yield return new WaitForSeconds(1.5f);
        nextProjectilCode.springJoint.enabled = true;
        nextProjectilCode.rigidBody.isKinematic = false;
        nextProjectile.SetActive(true);

        gameObject.SetActive(false);



        aiming = false;
        launching = false;
        playerLifes--;
        nextProjectilCode.playerLifes--;
    }
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (impactEffect)
        {
            impactEffect = false;
            Instantiate(impacteffect);
        }
        yield return new WaitForSeconds(0.39f);
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(0.5f);
        rigidBody.isKinematic = true;
        springJoint.enabled = false;
        gameObject.transform.position = tpAway;//Tp far away
        anim.SetBool("Death", false);
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        ColliderCircular.radius = 0.9f;
        yield return new WaitForSeconds(1.5f);

        //Projectil respawn code
        nextProjectilCode.rigidBody.isKinematic = true;
        nextProjectilCode.springJoint.enabled = false;
        nextProjectilCode.rigidBody.constraints = RigidbodyConstraints2D.None;
        nextProjectile.transform.position = projectilePosition;

        nextProjectilCode.impactEffect = true;
        nextProjectilCode.launchEffect = true;
        yield return new WaitForSeconds(1.5f);
        nextProjectilCode.springJoint.enabled = true;
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

