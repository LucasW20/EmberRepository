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
    private GameObject ember;
    private string[] tagsArray;

    // Start is called before the first frame update
    void Start() {
        //assign the global variables their objects
        ember = GameObject.Find("Ember");
        tagsArray = new string[]{"Drip", "Waterfall"};
    }

    //checks the type of collision to see if it's a wall or breaker to determine what to do.
    //OnTriggerEnter2D so that the projectile doesn't collide with the ember
    public void OnTriggerEnter2D(Collider2D collision) {
        //if the collision is with a wall
        if (collision.gameObject.CompareTag("Wall")) {
            //destroy the projectile and set the embers boolean to false so it can create another one
            Destroy(this.gameObject);
            ember.GetComponent<Shoot>().existingProjectile = false;
        }
        //if the collison is with a surface that can be broken
        else if (collision.gameObject.CompareTag("Breaker")) {
            //call destroyhandler
            collision.GetComponent<IceWallBehaviour>().DestroyHandler(gameObject);

            //then destroy the projectile
            Destroy(this.gameObject);        
            ember.GetComponent<Shoot>().existingProjectile = false;
        }   
        else if(compareAllTags(tagsArray, collision)) // check if the collision's tag matches the tag list
        {
            Destroy(this.gameObject);
            ember.GetComponent<Shoot>().existingProjectile = false;

        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    // white list of tags that destroy the projectile if hit
    private bool compareAllTags(string[] nTagsArray, Collider2D col)
    {
        for (int i = 0; i <= nTagsArray.Length - 1; i++)
        {
            if (col.gameObject.CompareTag(nTagsArray[i]))
            {
                return true;
            }
        }
        return false;
    }
}
