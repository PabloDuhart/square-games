using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private Animator anim;
    public float enemyContact = 3f;//enemy lifes
    public float distance;
    public AudioClip hitSound;
    private List<GameObject> nearbyEnemies;
    private bool deathBoy;
    private AudioSource soundsEffects;
    void Start()
    {
        anim = GetComponent<Animator>();
        soundsEffects = GetComponent<AudioSource>();
        anim.SetFloat("EnemyLife2", enemyContact);
        nearbyEnemies = new List<GameObject>();
        deathBoy = false;
    }
    private void Update()
    {
        var Enemys = GameObject.FindGameObjectsWithTag("enemy");
        foreach (var enem in Enemys)
        {
            if (enem != gameObject)
            {
                if (Vector3.Distance(enem.transform.position, gameObject.transform.position) <= distance && !deathBoy)
                {
                    nearbyEnemies.Add(enem);
                    var shield = enem.transform.GetChild(0).gameObject;
                    shield.SetActive(true);
                }
                else
                {
                    if (nearbyEnemies.Contains(enem))
                    {
                        nearbyEnemies.Remove(enem);
                        var shield = enem.transform.GetChild(0).gameObject;
                        shield.SetActive(false);
                    }

                }
                if (deathBoy)
                {
                    if (nearbyEnemies.Contains(enem))
                    {
                        nearbyEnemies.Remove(enem);
                        var shield = enem.transform.GetChild(0).gameObject;
                        shield.SetActive(false);
                    }
                }
            }
        }
    }
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil"))
        {
            //Enemy damaged, here you can put the animation.
            soundsEffects.clip = hitSound;
            soundsEffects.Play();
            enemyContact--;
            anim.SetBool("HitDamage2", true);
            anim.SetFloat("EnemyLife2", enemyContact);
        }
        if (collision.collider.CompareTag("EnemyStructure") && collision.relativeVelocity.magnitude > 1f)
        {
            enemyContact -= 0.5f;
            anim.SetBool("HitDamage2", true);
            anim.SetFloat("EnemyLife2", enemyContact);
            StartCoroutine(dmgwait2());
        }

        if (enemyContact < 0.1f)
        {
            deathBoy = true;
            //Enemy die, here you can put the animation.
            anim.SetFloat("EnemyLife2", enemyContact);
            yield return new WaitForSeconds(1.2f);
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil") || collision.collider.CompareTag("EnemyStructure"))
        {
            //Enemy damaged, here you can put the animation.

            anim.SetBool("HitDamage2", false);

        }
    }

    private IEnumerator dmgwait2()
    {
        yield return new WaitForSeconds(0.31f);
        if (anim.GetBool("HitDamage2"))
        {
            anim.SetBool("HitDamage2", false);
        }
    }
}
