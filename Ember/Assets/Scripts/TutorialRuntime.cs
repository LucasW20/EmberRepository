using System.Collections;
using UnityEngine;
using TMPro;

/***
 * Handles the runtime of the tutorial level
 * @author Lucas_C_Wright
 * @start 11-11-2021
 * @version 12-29-2021
 */
public class TutorialRuntime : MonoBehaviour {
    [SerializeField] TextMeshProUGUI ttlText; //object for the text in the canvas. set in unity
    private float fadeTime = 1f;
    private float waitTime = 1.5f;

    //serialized fields used for controlling interaction
    [SerializeField] GameObject health;
    [SerializeField] GameObject lives;
    [SerializeField] GameObject menuButt;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject windforce;
    [SerializeField] GameObject firstFire;
    [SerializeField] GameObject secondFire;
    [SerializeField] GameObject thirdFire;
    [SerializeField] GameObject ember;

    // Start is called before the first frame update
    void Start() {
        firstFire.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(StartTurorialCoroutine());
    }

    //checkpoint bools for which part in the tutorial the player is at
    private bool passFirst = false;
    private bool passSecond = false;
    private bool passThird = false;
    private bool passFinal = true;
    private bool checking = false;
    // Update is called once per frame
    void Update() {
        //check if the first fire is lit. if it is then pass the first game and start the next coroutine
        if (!passFirst && GameObject.Find("LeftFire").GetComponent<Campfire>().isFireLit()) {
            //update passFirst so this if statment only runs once
            passFirst = true;

            //start the next coroutine.
            SetNewNotification("Great! right click the fire to spawn the ember!", KeyCode.Space);
        }
        if (!passSecond && ember.GetComponent<PlayerHealth>().isEmberAlive()) {
            //update passFirst so this if statment only runs once
            passSecond = true;

            //activate the other fire and disable the first fire
            secondFire.SetActive(true);
            firstFire.GetComponent<BoxCollider2D>().enabled = false;
            ember.GetComponent<PlayerHealth>().FreezeHealth();  //freeze health so the player doesn't die when frozen at the disabled fire
            
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

        if (checking && ember.GetComponent<PlayerHealth>().getHealth() >= 23) {
            SetNewNotification("Looks like you ran out of health! In the game you would've lost a live, but we decided to be nice.", KeyCode.Space);
            ember.GetComponent<PlayerHealth>().resetTime();
        }
    }

    private IEnumerator notificationCoroutine;

    //display a notification. Passed a string of the text of the notification
    public void SetNewNotification(string message, KeyCode key) {
        //if there's a notification being displayed currently, stop it
        if (notificationCoroutine != null) {
            StopCoroutine(notificationCoroutine);
        }

        //set the current Coroutine and start it
        notificationCoroutine = FadeOutNotification(message, key);
        StartCoroutine(notificationCoroutine);
    }

    //the notification coroutine
    private IEnumerator FadeOutNotification(string message, KeyCode key) {
        ttlText.text = message;    //set the text of the gameobject to the message wanted
        float time = 0;                     //base time for when we start the fade out

        //set the base alpha and wait for the player to press space
        ttlText.color = new Color(ttlText.color.r, ttlText.color.g, ttlText.color.b, 1f);
        yield return WaitForKeyPress(key);

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

    //coroutines for teaching the player
    //first coroutine
    private IEnumerator StartTurorialCoroutine() {
        yield return new WaitForSeconds(6f);    //wait for the fade in to finish

        //display messages teaching the player
        SetNewNotification("Welcome to Ember!\n Press space to continue.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("Begin by right-clicking the logs to light the fire.", KeyCode.Space);
        firstFire.GetComponent<BoxCollider2D>().enabled = true;
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
    }

    //second coroutine
    public IEnumerator SecondCoroutine() {
        //freeze the player
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        checking = true;
        //display messages teaching the player
        SetNewNotification("In Ember you control the wind to blow around these embers.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("You must light fires to unlock the next level and gain new abilities.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("You can create wind by holding down the left mouse button and dragging across the screen.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("The meter in the top left shows the ember's time left until it fades. If it runs out you lose a life.", KeyCode.Space);
        health.SetActive(true);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("You can see how many lives you have in the top right.", KeyCode.Space);
        lives.SetActive(true);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("Return to fire to replenish the time. Time does not deplete while at a campfire.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);

        //unfreeze the player
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        firstFire.GetComponent<BoxCollider2D>().enabled = true;
        ember.GetComponent<PlayerHealth>().UnfreezeHealth();
        
        SetNewNotification("Now try to blow the ember around to find the other logs and light that fire.", KeyCode.Space);
        windforce.SetActive(true);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
    }

    //third coroutine
    public IEnumerator ThirdCoroutine() {
        //freeze the player again
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        ember.GetComponent<PlayerHealth>().FreezeHealth();
        secondFire.GetComponent<BoxCollider2D>().enabled = false;

        //display messages teaching the player
        SetNewNotification("Good job! For each fire lit you get 1 point. Gaining points will let you get new abilities!", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("Take note of the notification in the bottom.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("You've just unlocked the first ability in the game. When you get a new ability a similar message will show.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("For the wind projectile ability, aim with the mouse cursor and hit 'F' to fire. Try now!", KeyCode.F);
        yield return WaitForKeyPress(KeyCode.F);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("The wind projectile and other abilities will be used to help you navigate and progress!", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);

        secondFire.GetComponent<BoxCollider2D>().enabled = true;
        SetNewNotification("You also have the ability to douse fires. Hover the mouse over the fire and press 'N'. Try now.", KeyCode.N);
        yield return WaitForKeyPress(KeyCode.N);
        yield return new WaitForSeconds(waitTime);

        //unfreeze the player
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        ember.GetComponent<PlayerHealth>().UnfreezeHealth();

        SetNewNotification("Try blowing around the embers and using abilities until you're ready to proceed. Press SPACE when ready.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);

        //freeze the player again
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        ember.GetComponent<PlayerHealth>().FreezeHealth();
        secondFire.GetComponent<BoxCollider2D>().enabled = false;
        ember.GetComponent<Transform>().position = new Vector3(37.5f, 11f, 0);

        SetNewNotification("Nice! Next, the menu is used for various things like buying upgrades.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("Press 'ESC' to open the menu. Press SPACE after you've checked the menu and ready to continue.", KeyCode.Escape);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("While you're at a fire you can press 'Z' so see the entire level. Press 'Z' now.", KeyCode.Z);
        yield return WaitForKeyPress(KeyCode.Z);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("While you can see other fires you can right click them to move the ember to that fire. This will consume a life.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("If you run out of lives the level will restart. Move off of the fire to zoom back in.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("Take a minute to get used to the controls and using the menu. Press SPACE to finish the tutorial.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);

        passFinal = false; //unlock the last part
    }

    //final coroutine
    public IEnumerator FinalCoroutine() {
        //display messages teaching the player
        SetNewNotification("When you've lit enough fires in a level you'll get a message saying the pathway has opened.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        thirdFire.GetComponent<Campfire>().lightFire(); //light the last fire to open the pathway
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("A light will apear at the pathway and you'll have to move to that area to finish the level.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
        SetNewNotification("Move to the lighted area to exit the tutorial.", KeyCode.Space);
        yield return WaitForKeyPress(KeyCode.Space);
        yield return new WaitForSeconds(waitTime);
    }
}
