using UnityEngine;
using UnityEngine.UI;

/***
 * Attached to the Ember/Player to track the amount of points they have and abilities
 * @author Lucas_C_Wright
 * @start 10-13-2021
 * @version 12-12-2021
 */
public class PlayerPoints : MonoBehaviour {
    //class variables
    private int currPoints;
    private int pointsSpent;
    private int pointsEarned;
    PassingScene passingScene;
    NotificationManager ntManager;

    // Start is called before the first frame update
    private void Start() {
        //declare variables
        passingScene = GameObject.Find("SaveObject").GetComponent<PassingScene>();
        ntManager = GameObject.Find("MainUICanvas").GetComponent<NotificationManager>();

        //set the current number of points equal to the amount passed from the last scene. 0 for first level. 
        currPoints = passingScene.getCurrPoints();
        pointsEarned = passingScene.getTotalPoints();
    }

    //returns the amount of points the player has. Used for comparing to the abilities point requirement
    public int getCurrentPoints() { return currPoints; }
    public int getTotalPoints() { return pointsEarned; }
    public int getPointsSpent() { return pointsSpent; }

    //increments the amount of points the player has by 1. Used when a new fire is lit.
    public void incrementPoints() {
        //increment
        currPoints++;
        pointsEarned++;
        Debug.Log("Point gained! Total = " + pointsEarned);
        Debug.Log("Current =" + currPoints);

        //see if a new ability was unlocked
        CheckAbilityUnlock();

        //update the passing points 
        passingScene.passTotalPoints(1);
        passingScene.passCurrPoints(1);
    }

    //decrements the amount of points the player has by 1. Used when a fire is snuffed.
    public void decrementPoints(bool totalAndCurrent) { 
        //decrement
        currPoints--;
        if (totalAndCurrent)
        {
            pointsEarned--;
            passingScene.passTotalPoints(-1);
        }
        passingScene.passCurrPoints(-1);
        Debug.Log("Point lost. Total = " + pointsEarned);
        Debug.Log("Current =" + currPoints);
    }

    //checks if the player has gotten a new ability and displays a notification if they have
    private void CheckAbilityUnlock() {
        switch (pointsEarned) {
            case 2: //unlock wind projectile
                ntManager.SetNewNotification("Wind Projectile Ability Gained! Press F to use.");
                GameObject.Find("Wind Projectile Icon").GetComponent<WindProjectileIconController>().displayWindIcon();
                GameObject.Find("SaveObject").GetComponent<PassingScene>().displayWindProjectileIcon();
                break;
            case 7: //unlock long jump
                ntManager.SetNewNotification("Long Jump Ability Gained! Press G to use.");
                GameObject.Find("DashCounter").GetComponent<DashCounter>().displayDashCounter();
                GameObject.Find("SaveObject").GetComponent<PassingScene>().displayDashCountIcon();
                break;
            case 12: //unlock bubble shield
                ntManager.SetNewNotification("Bubble Shield Ability Unlocked! Press S to use.");
                break;
            default:
                //do nothing
                break;
        }
    }
}