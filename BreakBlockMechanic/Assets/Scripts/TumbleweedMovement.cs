using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleweedMovement : MonoBehaviour
{
    Rigidbody2D detector;
    GameObject ember;
    PolygonCollider2D tumblelider;
    //Force for the tumbleweed flies to the left
    [SerializeField] Vector2 force = new Vector2(-50, 0);

    // Start is called before the first frame update
    void Start()
    {
        //Finds the rigidbody
        detector = GetComponent<Rigidbody2D>();

        //Finds the ember game object
        ember = GameObject.Find("Ember");

        //Grabs the Polygon collider of the tumbleweed
        tumblelider = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the object that has gone into the trigger is the ember       
        if (collision.gameObject.name.Equals("Ember"))
        {
            //The tumbleweed is kinematic to so it doesn't move/fall 
            //Turn that off so it can move to the left
            detector.isKinematic = false;
            detector.drag = 0;
            detector.angularDrag = 0;
            detector.AddForce(force, ForceMode2D.Impulse);
            //Adds the force declared above so it falls fast

            //Starts the coroutine that is declared below
            StartCoroutine(IgnoreWallCoroutine());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the object that collides with the object is the ember, it destroys the tumbleweed
        if (collision.gameObject.name.Equals("Ember"))
        {
            ember.GetComponent<PlayerHealth>().deathEffect();
            Destroy(gameObject);
        }
       //If it doesn't hit the ember, it will delete after it hits a wall
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] 
    private float waitSeconds;

    //This is the coroutine that is called in the OnTriggerEnter
    //This will, as the name suggest, make the tumbleweed ignore the first collision with the wall
    public IEnumerator IgnoreWallCoroutine()
    {
        //yield on a new YieldInstruction that waits for 0.14 seconds before executing what is below
        yield return new WaitForSeconds(waitSeconds);

        //Collider is disabled to start so it doesn't delete itself when it flies through the wall. 
        //It is enabled once the 0.14 seconds pass
        tumblelider.enabled = true;
    }
}
