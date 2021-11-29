using System.Collections;
using UnityEngine;
using TMPro;

/***
 * Handles the runtime of the tutorial level
 * @author Lucas_C_Wright
 * @start 11-11-2021
 * @version 11-28-2021
 */
public class TutorialRuntime : MonoBehaviour {
    [SerializeField] TextMeshProUGUI ttlText; //object for the text in the canvas. set in unity
    [SerializeField] GameObject secondFire;
    [SerializeField] GameObject thirdFire;
    private float fadeTime = 1f;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(StartTurorialCoroutine());
    }

    //checkpoint bools for which part in the tutorial the player is at
    private bool passFirst = false;
    private bool passSecond = false;
    private bool passThird = false;
    private bool passFinal = true;
    // Update is called once per frame
    void Update() {
        //check if the first fire is lit. if it is then pass the first game and start the next coroutine
        if (!passFirst && GameObject.Find("LeftFire").GetComponent<Campfire>().isFireLit()) {
            //update passFirst so this if statment only runs once
            passFirst = true;

            //start the next coroutine.
            SetNewNotification("Great! right click the fire to spawn the ember!");
        }
        if (!passSecond && GameObject.Find("Ember").GetComponent<PlayerHealth>().isEmberAlive()) {
            //update passFirst so this if statment only runs once
            passSecond = true;

            //activate the other fire
            secondFire.SetActive(true);
            
            //start the next coroutine. 
            StartCoroutine(SecondCoroutine());
        }
        if (!passThird && secondFire.GetComponent<Campfire>().isFireLit()) {
            //update the bool and start the third part
            passThird = true;
            StartCoroutine(ThirdCoroutine());
        }
        if (!passFinal) {
            //update the bool and start the final part
            passFinal = true;
            StartCoroutine(FinalCoroutine());
        }
    }

    private IEnumerator StartTurorialCoroutine() {
        yield return new WaitForSeconds(6f);    //wait for the fade in to finish

        //display messages teaching the player
        SetNewNotification("Welcome to Ember!\n Press space to continue.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("Begin by right-clicking the logs to light the fire.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
    }

    public IEnumerator SecondCoroutine() {
        //display messages teaching the player
        SetNewNotification("In Ember you control the wind to blow around these embers.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("You must light fires to unlock the next level and gain new abilities.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("You can create wind by holding down the left mouse button and dragging across the screen.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("The meter in the top left shows the ember's time left until it fades. If it runs out you lose a life.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("You can see how many lives you have in the top right.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("Return to fire to replenish the time. Time does not deplete while at a campfire.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("Now try to blow the ember around to find the other logs and light that fire.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
    }

    public IEnumerator ThirdCoroutine() {
        //display messages teaching the player
        SetNewNotification("Good job! For each fire lit you get 1 point. Gaining points will let you get new abilities!");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("Take note of the notification in the bottom.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("You've just unlocked the first ability in the game. When you get a new ability a similar message will show.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("For the wind projectile ability, aim with the mouse cursor and hit 'F' to fire.");
        yield return WaitForKeyPress(KeyCode.F);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("The wind projectile and other abilities will be used to help you navigate and progress!");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("You also have the ability to douse fires. Hover the mouse over the fire and press the middle mouse button.");
        yield return WaitForKeyPress(KeyCode.Mouse2);
        yield return new WaitForSeconds(1.1f);
        //SetNewNotification("Keybinds for these abilities can be changed in the menu.");
        //yield return WaitForKeyPress(KeyCode.Space);
        //yield return new WaitForSeconds(1.1f);
        SetNewNotification("Try blowing around the embers and using abilities until you're ready to proceed. Press SPACE when ready.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("Nice! Next, the menu is used for various things like buying upgrades.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("Press 'P' to open the menu and 'P' again to close it. Press SPACE after you've checked the menu.");
        yield return WaitForKeyPress(KeyCode.P);
        yield return new WaitForSeconds(1.1f);
        yield return WaitForKeyPress(KeyCode.Space);
        SetNewNotification("While you're at a fire you can press 'Z' so see the entire level. Press 'Z' now.");
        yield return WaitForKeyPress(KeyCode.Z);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("While you can see other fires you can right click them to move the ember to that fire. This will consume a life.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("If you run out of lives the level will restart. Move off of the fire to zoom back in.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("Take a minute to get used to the controls and using the menu. Press SPACE to finish the tutorial.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);

        passFinal = false; //unlock the last part
    }

    public IEnumerator FinalCoroutine() {
        //display messages teaching the player
        SetNewNotification("When you've lit enough fires in a level you'll get a message saying the pathway has opened.");
        yield return WaitForKeyPress(KeyCode.Space);
        thirdFire.GetComponent<Campfire>().lightFire(); //light the last fire to open the pathway
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("A light will apear at the pathway and you'll have to move to that area to finish the level.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
        SetNewNotification("Move to the lighted area to exit the tutorial.");
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(1.1f);
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

        //set the base alpha and wait for the player to press space
        ttlText.color = new Color(ttlText.color.r, ttlText.color.g, ttlText.color.b, 1f);
        yield return WaitForKeyPress(KeyCode.Space);

        //loop to change the alpha gradually
        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;     //update the time
            ttlText.color = new Color(ttlText.color.r, ttlText.color.g, ttlText.color.b, Mathf.Lerp(1f, 0f, time / fadeTime));
            yield return null;                  //finish the loop
        }
    }

    private IEnumerator WaitForKeyPress(KeyCode key) {
        bool done = false;
        while (!done) {
            //if the correct key is pressed then stop the loop
            if (Input.GetKeyDown(key)) {
                done = true; // breaks the loop
            }
            //finish loop
            yield return null;
        }
    }
}
