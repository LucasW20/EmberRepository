using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Called when a fired projectile collides with a wall or breaker collider so that the projectile is destroyed and another can be fired.
 * 
 * @author Lucas_C_Wright
 * @start 10/07/2021
 * @version 10/10/2021
 */
public class DestroyProjectile : MonoBehaviour {
    //global object variables for ember and the projectile
    GameObject ember;
    GameObject thisProjectile;

    // Start is called before the first frame update
    void Start() {
        //assign the global variables their objects
        ember = GameObject.Find("Ember");
        thisProjectile = GameObject.Find("projectile(Clone)");
    }

    // Update is called once per frame
    void Update() { }

    //checks the type of collision to see if it's a wall or breaker to determine what to do.
    //OnTriggerEnter2D so that the projectile doesn't collide with the ember
    public void OnTriggerEnter2D(Collider2D collision) {
        //if the collision is with a wall
        if (collision.gameObject.CompareTag("Wall")) {
            //destroy the projectile and set the embers boolean to false so it can create another one
            Destroy(thisProjectile);
            ember.GetComponent<Shoot>().existingProjectile = false;
        }
        //if the collison is with a surface that can be broken
        else if (collision.gameObject.CompareTag("Breaker")) {
            //diable the breaker object
            collision.gameObject.SetActive(false);

            //play breaking sound
            AudioManager.PlaySound("breakingIce");

            //then destroy the projectile
            Destroy(thisProjectile);
            ember.GetComponent<Shoot>().existingProjectile = false;
        }
    }
}
