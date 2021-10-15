using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/***
 * Attached to the Ember/Player to track the amount of points they have and abilities
 * @author Lucas_C_Wright
 * @start 10/13/2021
 * @version 10/13/2021
 */
public class PlayerPoints : MonoBehaviour {

    private int currPoints;     //the total amount of points the player has during the game. determines which abilities the player can use. 
    public Text abilityText;
    Color textColor;

    void Start() {
        textColor = abilityText.color;   
    }

    //returns the amount of points the player has. Used for comparing to the abilities point requirement
    public int getPoints() { return currPoints; }

    //TODO implement fade in and fade out text letting the player know when they get a point and loose a point

    //increments the amount of points the player has by 1. Used when a new fire is lit.
    public void incrementPoints() {
        currPoints++;
        Debug.Log("Point gained! Total = " + currPoints);
    }

    //decrements the amount of points the player has by 1. Used when a fire is snuffed.
    public void decrementPoints() { 
        currPoints--;
        Debug.Log("Point lost. Total = " + currPoints);
    }
}