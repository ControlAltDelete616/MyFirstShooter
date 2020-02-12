using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float timeBetweenAttacks = 2f;
    public int attackDamage = 7;
    public AudioSource attackSound;
    public GameObject player;
    PlayerHealth playerHealth;
    bool isPlayerInRange;
    float timer;
    private Animator anim;


    void Awake()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    // Wird jeden Frame augefrufen
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && isPlayerInRange)
        {
            Attack();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            isPlayerInRange = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }

    //Methode welche die eigentliche Attacke ausführt
    void Attack()
    {
        timer = 0;

        if (attackSound != null)
            attackSound.Play();

        anim.Play("Attack");

        if (playerHealth.currentHealth >= 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}