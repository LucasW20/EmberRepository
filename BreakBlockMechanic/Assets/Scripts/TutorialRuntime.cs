using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/***
 * Handles the runtime of the tutorial level
 * @author Lucas_C_Wright
 * @start 11-11-2021
 * @version 11-18-2021
 */
public class TutorialRuntime : MonoBehaviour {
    [SerializeField] TextMeshProUGUI ttlText; //object for the text in the canvas. set in unity
    [SerializeField] GameObject secondFire;
    private float fadeTime = 1f;

    private bool firstGate = false;
    private bool secondGate = false;
    private bool finalGate = false;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(StartTurorialCoroutine());
    }

    // Update is called once per frame
    void Update() {
        if (GameObject.Find("LeftFire").GetComponent<Campfire>().isFireLit()) {
            firstGate = true;   //release the next gate to start the next coroutine.
            StartCoroutine(FirstGateCoroutine());
        }
        if (firstGate && GameObject.Find("Ember").GetComponent<PlayerHealth>().isEmberAlive()) {
            secondFire.SetActive(true);
            secondGate = true;
            StartCoroutine(FinalGateCoroutine());
        }
    }

    private IEnumerator StartTurorialCoroutine() {
        yield return new WaitForSeconds(6f);    //wait for the fade in to finish

        SetNewNotification("Welcome to Ember!");
        yield return new WaitForSeconds(2.5f);

        SetNewNotification("Begin by right-clicking the logs to light the fire.");
        yield return new WaitForSeconds(2.5f);
    }

    public IEnumerator FirstGateCoroutine() {
        SetNewNotification("Great! Your goal is to light fires to ");
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator FinalGateCoroutine() {
        SetNewNotification("Final Gate Cleared!");
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator notificationCoroutine;

    //display a notification. Passed a string of the text of the notification
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
        ttlText.text = message;    //set the text of the gameobject to the message wanted
        float time = 0;                     //base time for when we start the fade out

        //set the base alpha and wait for a few seconds to give player time to read
        ttlText.color = new Color(ttlText.color.r, ttlText.color.g, ttlText.color.b, 1f);
        yield return new WaitForSeconds(1.5f);

        //loop to change the alpha gradually
        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;     //update the time
            ttlText.color = new Color(ttlText.color.r, ttlText.color.g, ttlText.color.b, Mathf.Lerp(1f, 0f, time / fadeTime));
            yield return null;                  //finish the loop
        }
    }
}
