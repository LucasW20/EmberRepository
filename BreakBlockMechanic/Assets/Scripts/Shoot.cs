using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * When the player chooses to this script is called to create a projectile and fire it in the direction the player wants
 * 
 * @author Lucas_C_Wright
 * @start 10/05/2021
 * @version 10/10/2021
*/
public class Shoot : MonoBehaviour
{
    //global variables used for calculations and creating projectiles
    GameObject ember;   //the object for the ember
    public int moveForce;   //the amount of force applied to the projectile. Set in unity
    public GameObject prefab;   //the object used for the projectile. Set in unity
    GameObject nProjectile;     //the object for the projectile
    Rigidbody2D projecPhys;     //the physics engine for the projectile
    private int pointRequirement = 2;    //the required amount of points to use this ability
    
    // Start is called before the first frame update
    void Start() {
        //get the object for the fire
        ember = GameObject.Find("Ember");
    }

    public bool existingProjectile = false;

    // Update is called once per frame
    void Update() {
        //if the player presses the F key then shoot a projectile 
        if (Input.GetKeyDown("f") && !existingProjectile && ember.GetComponent<PlayerPoints>().getPoints() >= pointRequirement) {
            //log for debugging
            //Debug.Log("Clicked F for FIRE!");

            existingProjectile = true;

            //first create the object using the prefab at the embers location
            nProjectile = Instantiate(prefab, ember.transform.position, ember.transform.rotation);
            //then get the projectiles phyisics engine
            projecPhys = nProjectile.GetComponent<Rigidbody2D>();

            //nProjectile.transform.Rotate(calculateAngle());

            //then apply the force based on where the mouse location is
            projecPhys.AddForce((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * moveForce, ForceMode2D.Impulse);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(nProjectile);
            existingProjectile = false;
        }
    }

    ////change to work for projectile
    //private float calculateAngle() {
    //    // set default angle to 90 (maybe bad practice)
    //    float angle = 90;
    //    float slope;

    //    Vector2 point1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 point2 = ember.transform.position;

    //    // slope of line between mouse origin and mouse current
    //    if (point2.x != point1.x) {
    //        slope = ((point2.y - point1.y) / (point2.x - point1.x));

    //        // if the line is going to the left or straight up, angle = 0 + angle
    //        if (point1.x - point2.x <= 0) {
    //            // calculates angle as ArcTan of slope. Atan returns answer in radians. Multiplies by 180/pi to convert to degrees
    //            angle = (Mathf.Atan(slope) * 180 / Mathf.PI);
    //        }

    //        // if the line is going to the right, angle = 180 + angle
    //        else {
    //            // calculates angle as ArcTan of slope. Atan returns answer in radians. Multiplies by 180/pi to convert to degrees
    //            angle = 180 + Mathf.Atan(slope) * 180 / Mathf.PI;
    //        }
    //    } else if (point2.y > point1.y) {
    //        angle = 90;
    //    } else if (point2.y <= point1.y) {
    //        angle = 270;
    //    }

    //    // return the angle
    //    return angle;
    //}
}
