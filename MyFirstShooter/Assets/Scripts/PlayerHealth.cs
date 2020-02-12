using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public Slider healthSlider;
    public Image damageImage;
    public Color flashColor = new Color(1, 0, 0, 0.3f);
    public float damageImageFlashSpeed = 20f;
    public AudioSource deathScream;
    bool isDead;
    bool takesDamage;

    void Awake()
    {
        healthSlider.maxValue = currentHealth;
        isDead = false;
    }

    public void TakeDamage(int dmg)
    {
        takesDamage = true;
        currentHealth -= dmg;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }


    void Die()
    {

        isDead = true;

        if (deathScream != null) { }
        deathScream.Play();

        StartCoroutine("MuteAllSound");

        StartCoroutine("OnDeath");
    }

    IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    IEnumerator MuteAllSound()
    {
        yield return new WaitForSeconds(2f);
        AudioListener.volume = 0f;
    }



    void Update()
    {

        if (!isDead)
        {

            if (takesDamage)
            {

                damageImage.color = flashColor;
            }
            else
            {

                damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageImageFlashSpeed * Time.deltaTime);
            }
        }
        else
        {

            Color newColor = new Color(1f, 0, 0);
            newColor.a = damageImage.color.a + 0.1f;
            damageImage.color = Color.Lerp(damageImage.color, newColor, 2000f);
        }
        takesDamage = false;

    }
}