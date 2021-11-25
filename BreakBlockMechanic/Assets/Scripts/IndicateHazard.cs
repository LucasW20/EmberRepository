using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//detects when a indicator sound should be played 
public class IndicateHazard : MonoBehaviour {
    bool played = false;
    [SerializeField] FallingIce fallingIce;

    //playes the indicator sound
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && !played) {
            AudioManager.PlaySound("iceIndicator");
            played = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Ember"))
        {
            fallingIce.wiggle();
        }
    }

}