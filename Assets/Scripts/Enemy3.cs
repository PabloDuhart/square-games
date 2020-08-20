using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    private Animator anim;
    public float enemyContact;//enemy lifes
    public EdgeCollider2D topCollider;
    private Rigidbody2D otherRb;
    private bool canBeDmged;
    private bool canBeD;
    private EdgeCollider2D UpCollider;
    public AudioClip hitSound;
    private AudioSource soundEffects;
    void Start()
    {
        anim = GetComponent<Animator>();
        soundEffects = GetComponent<AudioSource>();
        anim.SetFloat("EnemyLife3", enemyContact);
    }

    private void Update()
    {
        var shield = transform.GetChild(0).gameObject;
        if (shield.activeSelf)
        {
            var codechild = shield.GetComponent<littleEnemy>();
            canBeD = codechild.abletobedmg;
        }
        else
        {
            canBeD = true;
        }
    }
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (canBeDmged && canBeD) { 
            if (collision.collider.CompareTag("projectil"))
            {
                //Enemy damaged, here you can put the animation.
                soundEffects.clip = hitSound;
                soundEffects.Play();
                enemyContact--;
                anim.SetBool("HitDamage3", true);
                anim.SetFloat("EnemyLife3", enemyContact);
            }
            if (collision.collider.CompareTag("EnemyStructure") && collision.relativeVelocity.magnitude > 1f && canBeD)
            {
                enemyContact -= 0.2f;
                anim.SetBool("HitDamage3", true);
                anim.SetFloat("EnemyLife3", enemyContact);
                StartCoroutine(dmgwait3());
            }
        }
        if (enemyContact < 0.1f)
        {
            //Enemy die, here you can put the animation.
            anim.SetFloat("EnemyLife3", enemyContact);
            yield return new WaitForSeconds(1.2f);
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil") || collision.collider.CompareTag("EnemyStructure"))
        {
            //Enemy damaged, here you can put the animation.

            anim.SetBool("HitDamage3", false);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EdgeCollider2D>() == UpCollider)
        {
            canBeDmged = false;
            if (collision.gameObject.CompareTag("EnemyStructure"))
            {
                anim.SetBool("Attack", true);

                otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
                int randompositionX = new System.Random().Next(-200, 200);
                otherRb.AddForce(new Vector2(randompositionX, 1000f));
            }
            if (collision.gameObject.CompareTag("enemy"))
            {
                anim.SetBool("Attack", true);
                otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
                int randompositionX = new System.Random().Next(-2000, 2000);
                otherRb.AddForce(new Vector2(randompositionX, 10000f));
            }
        }
        if (collision.CompareTag("projectil") && canBeD){
            soundEffects.clip = hitSound;
            soundEffects.Play();
            enemyContact--;
            anim.SetBool("HitDamage3", true);
            anim.SetFloat("EnemyLife3", enemyContact);
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        canBeDmged = true;
        if (collision.gameObject.CompareTag("EnemyStructure") || collision.gameObject.CompareTag("enemy"))
        {
            yield return new WaitForSeconds(0.15f);
            anim.SetBool("Attack", false);
        }
    }
    private IEnumerator dmgwait3()
    {
        yield return new WaitForSeconds(0.16f);
        if (anim.GetBool("HitDamage3"))
        {
            anim.SetBool("HitDamage3", false);
        }
    }
}
