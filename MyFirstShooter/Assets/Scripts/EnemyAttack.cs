using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Zeit zwischen Attacken
    public float timeBetweenAttacks = 2f;
    // Schaden den eine Attacke zufügt
    public int attackDamage = 7;
    // Sound der bei einer Attacke abgespielt werden soll
    public AudioSource attackSound;
    // Das Spielerobjekt
    public GameObject player;
    // Das PlayerHealth Script
    PlayerHealth playerHealth;
    // Helpervariable die sagt ob der Spieler in Reichweite für eine Attacke ist
    bool isPlayerInRange;
    // Der Timer den wir nutzen um zu sehen ob es wieder Zeit für eine Attacke ist
    float timer;
    // Der Animator des Gegners
    private Animator anim;


    void Awake()
    {
        // Wir holen uns den Gegner 
        // Das ist eine sehr resourcenhungrige Methode, also niemals in Update o.ä. aufrufen
        player = GameObject.Find("Player");
        // Wir holen uns das PlayerHealth Script vom Player
        playerHealth = player.GetComponent<PlayerHealth>();
        // Wir holen uns den Animator vom Enemy
        anim = GetComponent<Animator>();
    }

    // Wird jeden Frame augefrufen
    void Update()
    {
        // wir setzen den Timer auf den alten Wert + die Zeit die seit dem letzten
        // Frame vergangen ist
        timer += Time.deltaTime;
        // Wenn zeit für einen Attack ist, und der Gegner in Reichweit ist
        if (timer >= timeBetweenAttacks && isPlayerInRange)
        {
            // attackieren wir
            Attack();
        }
    }

    // Wenn der Gegner in Reichweite des Spielers ist, wird "isPlayerInRange" auf true gesetzt
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            isPlayerInRange = true;
        }
    }

    // Wenn der Gegner nicht mehr in Reichweite des Spielers ist, wird "isPlayerInRange" auf false gesetzt
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }

    // Unsere Methode welche die eigentliche Attacke ausführt
    void Attack()
    {
        // Wir resetten den Timer, denn wir greifen ja gerade an und sollen erst wieder 
        // in "timeBetweenAttacks" angreifen
        timer = 0;
        // Wenn der Attacksound existiert, führen wir ihn aus
        if (attackSound != null)
            attackSound.Play();

        // Wir spielen die Attack Animation aus
        // Man könnte hierbei überlegen, ob wir das nicht auch über einen Bool lösen,
        // wie wir es in "EnemyCollisionController" machen. 
        // Langfristig auf jeden Fall zu empfehlen.
        anim.Play("Attack");

        // Solange der Spieler noch leben hat
        if (playerHealth.currentHealth >= 0)
        {
            // Rufen wir seine TakeDamage Methode auf und fügen "attackDamage" Menge an Schaden zu
            playerHealth.TakeDamage(attackDamage);
        }
    }
}