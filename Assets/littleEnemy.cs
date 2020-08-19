using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleEnemy : MonoBehaviour
{
    public bool abletobedmg;
    // Start is called before the first frame update
    void Start()
    {
        abletobedmg = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("projectil"))
        {
            var code = collision.gameObject.GetComponent<projectile_Script>();
            code.Death();
        }   
    }
}
