using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceshipfathershield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("projectil"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("EnemyStructure") || collision.gameObject.CompareTag("enemy"))
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject spaceshipshield = gameObject.transform.GetChild(i).gameObject;
                spaceshipshield.SetActive(true);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("projectil"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("EnemyStructure") || collision.gameObject.CompareTag("enemy"))
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject spaceshipshield = gameObject.transform.GetChild(i).gameObject;
                spaceshipshield.SetActive(false);
            }
        }
    }
}
