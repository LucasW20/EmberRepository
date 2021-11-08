using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

/*
 * @version 10-28-2021
 */
public class Campfire : MonoBehaviour {
    GameObject ember;
    GameObject scriptHolder;
    LightFirstFire lightFirstFire_SH;
    Vector2 fireLocation;

    bool isLit;
    bool mouseIsOver;

    Vector2 unLitBoxColOffset;
    Vector2 unLitBoxColSize;
    Vector2 litBoxColOffset;
    Vector2 litBoxColSize;

    // Start is called before the first frame update
    void Start() {
        ember = GameObject.Find("Ember");
        scriptHolder = GameObject.Find("ScriptHolder");
        lightFirstFire_SH = scriptHolder.GetComponent<LightFirstFire>();

        fireLocation = this.transform.position;
        mouseIsOver = false;
        isLit = false;

        unLitBoxColOffset = new Vector2(0, -1.8f);
        unLitBoxColSize = new Vector2(3, 1.5f);
        litBoxColOffset = new Vector2(0, 0f);
        litBoxColSize = new Vector2(3, 5f);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse2)) // if middle mouse button is pressed
        {
            if (isLit && mouseIsOver) {
                snuffFire();
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)) // if right mouse button is pressed
        {
            if(!isLit && mouseIsOver)
            {
                lightFirstFire_SH.lightFirstFire(this);
            }
            else if (isLit && mouseIsOver)
            {
                ember.GetComponent<PlayerHealth>().revive(fireLocation);
            }
        }
    }

    private void OnMouseEnter() {
        mouseIsOver = true;
    }

    private void OnMouseExit() {
        mouseIsOver = false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!isLit) {
            if (collision.gameObject.CompareTag("Player")) {
                Debug.Log("The fire is lit!");
                lightFire();
            }
        }
    }

    public void lightFire() {
        // if fire is not lit, set it to lit
        if (!isLit) { 
            isLit = true;   //its lit bro
            this.GetComponent<Animator>().SetBool("lightFire", true);
            this.GetComponent<Animator>().SetBool("snuffFire", false);
            scriptHolder.GetComponent<CampfireTracker>().increaseNumFiresLit();
            Debug.Log(scriptHolder.GetComponent<CampfireTracker>().getNumfiresLit());

            //send out the notification before the points increase so that it doesn't interrupt ability gained notifications
            StartCoroutine(PointNotification());

            //increment the player points
            ember.GetComponent<PlayerPoints>().incrementPoints();

            //when the fire is lit change the embers last fire lit variable to this one
            GameObject.Find("Ember").GetComponent<PlayerHealth>().lastFire = this;

            //when lit restore the embers time/health
            GameObject.Find("Ember").GetComponent<PlayerHealth>().resetTime();

            // change collider size
            GetComponent<BoxCollider2D>().size = litBoxColSize;
            GetComponent<BoxCollider2D>().offset = litBoxColOffset;
            // make fire blow upwards
            GetComponent<AreaEffector2D>().enabled = true;

            //play ignite fire and continuous fire 
            AudioManager.PlaySound("ignite");
            GetComponent<AudioSource>().mute = false;

            gameObject.GetComponent<Light2D>().enabled = true;
        }
    }

    public void snuffFire() {
        //TODO: Currently if you snuff out the last lit fire and then die the camera will be set on a campfire that can't
        //be relight. Essensially softlocking the game. 
        if (scriptHolder.GetComponent<CampfireTracker>().getNumfiresLit() > 1 && isLit) {
            Debug.Log("snuffFire is being called");
            isLit = false;
            this.GetComponent<Animator>().SetBool("snuffFire", true);
            this.GetComponent<Animator>().SetBool("lightFire", false);
            scriptHolder.GetComponent<CampfireTracker>().decreaseNumFireLit();
            Debug.Log(scriptHolder.GetComponent<CampfireTracker>().getNumfiresLit());

            //decrement the player's total and current points
            ember.GetComponent<PlayerPoints>().decrementPoints(true);

            // change collider size
            GetComponent<BoxCollider2D>().size = unLitBoxColSize;
            GetComponent<BoxCollider2D>().offset = unLitBoxColOffset;
            // disable upward wind effect
            GetComponent<AreaEffector2D>().enabled = false;

            //stop the fire sound and play the snuff fire sound
            GetComponent<AudioSource>().mute = true;
            AudioManager.PlaySound("snuffFire");

            gameObject.GetComponent<Light2D>().enabled = false;
        }
    }

    // Coroutine for when a campfire is lit. Displays the notification by the campfire and fades out the text
    IEnumerator PointNotification() {
        TextMeshPro CFText = gameObject.GetComponentInChildren<TextMeshPro>(); //get the text component
        float fadeTime = 2f; //time to fade
        float time = 0; //starting time
        CFText.color = new Color(CFText.color.r, CFText.color.g, CFText.color.b, 1f); //set the color to visible 

        //wait for a few seconds for player to read
        yield return new WaitForSeconds(1.5f);

        //fade out
        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;
            CFText.color = new Color(CFText.color.r, CFText.color.g, CFText.color.b, Mathf.Lerp(1f, 0f, time / fadeTime));
            yield return null;
        }
    }
}
