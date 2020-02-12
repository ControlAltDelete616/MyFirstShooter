using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemyCollisionController : MonoBehaviour
{
    public int health = 5;

    Text enemiesKilledAmountText;
    public AudioSource deathSound;
    private Animator anim;
    private Collider collider;
    public float delay = 0.5f;

    private void Awake()
    {
        GameObject gaOb = GameObject.Find("EnemiesKilledValue");
        enemiesKilledAmountText = gaOb.GetComponent<Text>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            health--;
            if(health < 1)
            {
                GetComponent<EnemyAttack>().enabled = false;
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
                GetComponent<EnemyMovementController>().enabled = false;
                collider.enabled = false;

                int kills;
                System.Int32.TryParse(enemiesKilledAmountText.text, out kills);
                kills++;
                enemiesKilledAmountText.text = kills.ToString();
                deathSound.Play();
                anim.SetBool("enemyIsDead", true);
                Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + delay);
            }
        }
    }
}
