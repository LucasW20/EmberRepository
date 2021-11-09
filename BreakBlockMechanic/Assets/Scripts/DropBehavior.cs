using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehavior : MonoBehaviour
{
    PlayerHealth playerHealth;
    private float timePassed;
    [HideInInspector]
    public int meltingPoint;
    private int firesLit;
    private bool frozen = false;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Ember").GetComponent<PlayerHealth>();
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        firesLit = GameObject.Find("ScriptHolder").GetComponent<CampfireTracker>().getNumfiresLit();
        timePassed += Time.deltaTime;
        // This destroys the object after 10 seconds, just in case
        if(timePassed > 10 && firesLit >= meltingPoint)
        {
            Destroy(this.gameObject);
        }
        if (firesLit < meltingPoint)
        {
            GetComponent<Collider2D>().isTrigger = false;
            frozen = true;
        }
        else
        {
            GetComponent<Collider2D>().isTrigger = true;
            frozen = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!collision.gameObject.CompareTag("Drip")) {

            // if the object collides with the player
            if (collision.gameObject.CompareTag("Player")) {
                // kill the player
                playerHealth.deathEffect();
            }
            // if the drop hits anything, destroy itself.
            if (firesLit >= meltingPoint)
            {
                Destroy(this.gameObject);
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Breaker")) {
            collision.gameObject.GetComponent<IceWallBehaviour>().DestroyHandler(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public bool isFrozen() {
        return frozen;
    }
}
