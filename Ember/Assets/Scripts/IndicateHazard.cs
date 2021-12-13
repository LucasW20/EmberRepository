using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * detects when a indicator sound should be played 
 * @author Lucas_C_Wright
 * @start 10-18-2021
 * @version 12-12-2021
 */
public class IndicateHazard : MonoBehaviour {
    bool played = false;
    [SerializeField] GameObject fallingIce;
    [SerializeField] bool ice;

    //playes the indicator sound
    public void OnTriggerEnter2D(Collider2D collision) {
        //if there is a collision with only the player and the indication has only played once then play a sound
        if (collision.gameObject.CompareTag("Player") && !played) {
            //if its ice then play the ice sound, otherwise play the wind sound
            if (ice) {
                AudioManager.PlaySound("iceIndicator");
            } else {
                AudioManager.PlaySound("wind");
            }

            //update played boolean
            played = true;
        }
    }

    //starts the wiggle animation for when the player is near
    public void OnTriggerStay2D(Collider2D collision) {
        if(ice && collision.gameObject.name.Equals("Ember")) {
            fallingIce.GetComponent<FallingIce>().wiggle();
        }
    }

}