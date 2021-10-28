using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/***
 * Manages notifications send by other classes and displays it on the canvas for a time.
 * 
 * @author Lucas_C_Wright
 * @start 10-24-2021
 * @version 10-24-2021
 */
public class NotificationManager : MonoBehaviour {

    /*
     * HOW TO ADD A NOTIFICATION:
     * 1. Find in code when you want a notification displayed (i.e. lighting campfire)
     * 2. Declare a variable for a manager in your class. (example: NotificationManager ntManager;)
     * 3. Put "ntManager = GameObject.Find("MainUICanvas").GetComponent<NotificationMangager>();" in the start method of that class
     * 4. Call "ntManager.SetNewNotification("message");" where message is your string for what you want the notification to display
     */

    //variables used for the text
    [SerializeField] private TextMeshProUGUI notificationText;  //the gameobject for the text. Set in unity
    [SerializeField] private float fadeTime = 3.5f;             //the time in seconds it takes for the fade out to take. Set in unity.
    private IEnumerator notificationCoroutine;                  //variable to hold the current Coroutine

    //method called by other classes to display a notification. Passed a string of the text of the notification
    public void SetNewNotification(string message) {
        //if there's a notification being displayed currently, stop it
        if (notificationCoroutine != null) {
            StopCoroutine(notificationCoroutine);
        }

        //set the current Coroutine and start it
        notificationCoroutine = FadeOutNotification(message);
        StartCoroutine(notificationCoroutine);
    }

    //the coroutine method
    private IEnumerator FadeOutNotification(string message) {
        notificationText.text = message;    //set the text of the gameobject to the message wanted
        float time = 0;                     //base time for when we start the fade out

        //loop to change the alpha gradually
        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;     //update the time
            notificationText.color = new Color(notificationText.color.r,    //update the color
                                  notificationText.color.g,
                                  notificationText.color.b,
                                  Mathf.Lerp(1f, 0f, time / fadeTime));
            yield return null;                  //finish the loop
        }
    }
}