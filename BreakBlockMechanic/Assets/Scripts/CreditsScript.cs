using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Handles the unique behaviour of the credits level.
 * @author Lucas_C_Wright
 * @start 11-29-21
 * @version 11-29-21
 */
public class CreditsScript : MonoBehaviour {
    private PlayerHealth ember;

    // Start is called before the first frame update
    void Start() {
        ember = GameObject.Find("Ember").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update() {
        ember.resetTime();
    }
}
