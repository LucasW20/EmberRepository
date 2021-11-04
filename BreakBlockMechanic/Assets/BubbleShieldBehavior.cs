using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShieldBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            StartCoroutine(startShieldCoroutine());
        }
    }

    public IEnumerator startShieldCoroutine()
    {
        // toggles a boolean in playerhealth, stopping health loss from collisions
        GetComponent<PlayerHealth>().toggleShield(true);
        // increase the collider radius
        GetComponent<CircleCollider2D>().radius = 1.5f;
        yield return new WaitForSeconds(3);
        // disable shield and set radius back to normal
        GetComponent<PlayerHealth>().toggleShield(false);
        GetComponent<CircleCollider2D>().radius = .76f;
    }
}
