using UnityEngine;
using UnityEngine.UI;

/***
 * Attached to the Ember/Player to track the amount of points they have and abilities
 * @author Lucas_C_Wright
 * @start 10-13-2021
 * @version 10-28-2021
 */
public class PlayerPoints : MonoBehaviour {

    private int currPoints;     //the total amount of points the player has during the game. determines which abilities the player can use. 
    private int pointsSpent;
    private int pointsEarned;
    PassingScene passingScene;
    NotificationManager ntManager;  

    private void Start() {
        //declare variables
        passingScene = GameObject.Find("SaveObject").GetComponent<PassingScene>();
        ntManager = GameObject.Find("MainUICanvas").GetComponent<NotificationManager>();

        //set the current number of points equal to the amount passed from the last scene. 0 for first level. 
        currPoints = passingScene.getPoints();
    }
    private void Update() {
        //cheat code! get 1000 points. used for when the devs dont wanna collect all them points to test something. REMOVE FOR FINAL
        if (Input.GetKeyDown("l")) {
            currPoints = 1000;
            Debug.Log("1k points gained! Cheater!");
        }

        if (Input.GetKeyDown("c")) {
            gameObject.GetComponent<PlayerHealth>().resetTime();
        }
    }

    //returns the amount of points the player has. Used for comparing to the abilities point requirement
    public int getCurrentPoints() { return currPoints; }

    public int getTotalPoints() { return pointsEarned; }

    public int getPointsSpent() { return pointsSpent; }

    //TODO implement fade in and fade out text letting the player know when they get a point and loose a point

    //increments the amount of points the player has by 1. Used when a new fire is lit.
    public void incrementPoints() {
        currPoints++;
        pointsEarned++;
        Debug.Log("Point gained! Total = " + currPoints);

        CheckAbilityUnlock();

        passingScene.passPoints(1);
    }

    //decrements the amount of points the player has by 1. Used when a fire is snuffed.
    public void decrementPoints(bool totalAndCurrent) { 
        currPoints--;
        if (totalAndCurrent)
        {
            pointsEarned--;
        }
        passingScene.passPoints(-1);
        Debug.Log("Point lost. Total = " + currPoints);
    }


    private void CheckAbilityUnlock() {
        switch (currPoints) {
            case 2:
                ntManager.SetNewNotification("Wind Projectile Ability Gained! Press F to use.");
                break;
            case 7:
                ntManager.SetNewNotification("Long Jump Ability Gained! Press G to use.");
                break;
            default:
                //do nothing
                break;
        }
    }
}