using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Handles the behaviour for the melted part of the waterfall
 * 
 * @author Lucas_C_Wright
 * @start 12-02-21
 * @version 12-02-21
 */
public class MeltedWaterfallBehaviour : MonoBehaviour {
    private bool falling = false;
    private GameObject ember;

    // Start is called before the first frame update
    void Start() {
        ember = GameObject.Find("Ember");
    }

    //when the ember enters the water start the 
    private void OnTriggerEnter2D(Collider2D collision) {
        falling = true;
        StartCoroutine(WaterfallCoroutine());
    }

    private void OnTriggerExit2D(Collider2D collision) {
        falling = false;
        StopCoroutine(WaterfallCoroutine());
    }

    private IEnumerator WaterfallCoroutine() {
        while (falling) {
            if (ember.GetComponent<PlayerHealth>().getHealth() >= 24) {
                falling = false;
            }
            StartCoroutine(ember.GetComponent<PlayerHealth>().LoseHealthCoroutine(1, 1));
            ember.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -7.5f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
