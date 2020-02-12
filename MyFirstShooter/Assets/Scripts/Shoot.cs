using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    
    public GameObject bullet;
    public float speed = 2000f;
    public float shootRate = 0.1f;
    public AudioSource shootSound;

    float timer = 0f;


    void Update()
    {

        timer += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (timer >= shootRate)
            {
                GameObject g = Instantiate(bullet, transform.position, transform.parent.rotation);
                g.GetComponent<Rigidbody>().AddForce(g.transform.forward * -speed);
                timer = 0;
            }
            if (shootSound != null && !shootSound.isPlaying)
                shootSound.Play();
        }
        else
        {
           // if (shootSound != null)
               // shootSound.mute = true;
        }

    }
}