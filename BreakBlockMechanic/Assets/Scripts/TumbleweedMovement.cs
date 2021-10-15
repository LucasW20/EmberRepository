using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleweedMovement : MonoBehaviour
{
    Rigidbody2D detector;
    GameObject ember;
    CircleCollider2D tumblelider;
    Vector2 force = new Vector2(-75, 0);

    // Start is called before the first frame update
    void Start()
    {
        //Finds the rigidbody
        detector = GetComponent<Rigidbody2D>();

        //Finds the ember game object
        ember = GameObject.Find("Ember");

        //
        tumblelider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the object that has gone into the trigger is the ember       
        if (collision.gameObject.name.Equals("Ember"))
        {
            //The ice is kinematic to so it stays on the ceiling. 
            //Turn that off so it can fall
            detector.isKinematic = false;
            detector.drag = 0;
            detector.angularDrag = 0;
            detector.AddForce(force, ForceMode2D.Impulse);
            //Adds the force declared above so it falls fast

            StartCoroutine(IgnoreWallCoroutine());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the object that collides with the object is the ember, it destroys the ice
        if (collision.gameObject.name.Equals("Ember"))
        {
            ember.GetComponent<PlayerHealth>().deathEffect();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator IgnoreWallCoroutine()
    {
        //yield on a new YieldInstruction that waits for 1 second.
        yield return new WaitForSeconds(0.12f);

        tumblelider.enabled = true;
    }
}
