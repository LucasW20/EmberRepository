using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIce : MonoBehaviour
{
    Rigidbody2D Detector;
    GameObject ember;

    bool movingLeft = true;
   
    //Sets the force for the ice to fall
    Vector2 force = new Vector2(0, -50);

    // Start is called before the first frame update
    void Start() {
        //Grabs the ice's rigid body
        Detector = GetComponent<Rigidbody2D>();
        ember = GameObject.Find("Ember");
    }

    public void wiggle()
    {
            Vector3 tempVec = transform.position;
            if(movingLeft)
            {
                tempVec.x += .1f;
                transform.position = tempVec;
                movingLeft = false;
            }
            else
            {
                tempVec.x -= .1f;
                transform.position = tempVec;
                movingLeft = true;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //If the object that has gone into the trigger is the ember       
        if (collision.gameObject.name.Equals("Ember")) {
            //The ice is kinematic to so it stays on the ceiling. 
            //Turn that off so it can fall
            Detector.isKinematic = false;
            Detector.drag = 0;
            Detector.angularDrag = 0;
            Detector.AddForce(force, ForceMode2D.Impulse);
            //Adds the force declared above so it falls fast

            //play the falling ice sound
            AudioManager.PlaySound("IceFall");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //If the object that collides with the object is the ember, it destroys the ice
        if (collision.gameObject.name.Equals("Ember")) {
            ember.GetComponent<PlayerHealth>().deathEffect();
        } else if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}