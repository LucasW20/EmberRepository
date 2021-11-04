using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShieldBehavior : MonoBehaviour
{
    GameObject ember;
    // Start is called before the first frame update
    void Start()
    {
        ember = GameObject.Find("Ember");
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.position = ember.transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        }
    }
}
