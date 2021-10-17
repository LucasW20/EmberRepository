using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehavior : MonoBehaviour
{
    PlayerHealth playerHealth;
    private float timePassed;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Ember").GetComponent<PlayerHealth>();
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        // This destroys the object after 10 seconds, just in case
        if(timePassed > 10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //play drip sound
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("droplet"));
        
        // if the object collides with the player
        if(collision.gameObject.CompareTag("Player"))
        {
            // kill the player
            playerHealth.deathEffect();
        }
        // if the drop hits anything, destroy itself.
        Destroy(this.gameObject);
    }
}
