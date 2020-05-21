using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_Controller : MonoBehaviour
{
    private Animator anim;
    public Vector2 start;
    public Vector2 preLanding;
    public Vector2 ground;
    public float startDelay;
    private float distance;
    private bool preLand;
    public Vector3 spaceShipScale;
    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);//Change scale of spaceShip
        gameObject.transform.rotation = new Quaternion(0f, 0f, -45, 110);//Change rotation of spaceShip
        gameObject.transform.localPosition = start;//Change position of spaceShip
        anim.SetBool("Grounded", false);
        preLand = false;
        distance = 0;
        anim.SetBool("Flying", true);
    }

    // Update is called once per frame
    void Update()
    {   
        if (gameObject.transform.localPosition.Equals(preLanding))
        {
            preLand = true;
            gameObject.transform.localScale = spaceShipScale;
            gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            distance = 0.01f;
        }
        else
        {
            gameObject.transform.localPosition = Vector2.MoveTowards(start, preLanding, distance);
            distance += 0.01f;
        }
        if (preLand)
        {
            gameObject.transform.localPosition = Vector2.MoveTowards(preLanding, ground, distance);
            distance += 0.01f;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log("entra al collision");
            preLand = false;
            gameObject.transform.localPosition = Vector2.MoveTowards(preLanding, ground, distance);
            distance += 0.01f;
            anim.SetBool("Flying", false);
           
            anim.SetBool("Grounded", true);
        }
    }
}
