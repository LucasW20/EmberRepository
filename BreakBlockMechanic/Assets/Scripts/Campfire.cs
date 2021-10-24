using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    NotificationManager ntManager;

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

        ntManager = GameObject.Find("MainUICanvas").GetComponent<NotificationManager>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            if (isLit && mouseIsOver) {
                snuffFire();
            }
        }
    }

    private void OnMouseEnter() {
        mouseIsOver = true;
    }

    private void OnMouseExit() {
        mouseIsOver = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!isLit) {
            if (collision.gameObject.CompareTag("Player")) {
                Debug.Log("The fire is lit!");
                lightFire();
            }
        }
    }

    private void OnMouseDown() {
        Debug.Log("Clicked!");
        if(!isLit) {
           lightFirstFire_SH.lightFirstFire(this);
        }
    }

    private void OnMouseUp()
    {
        if (isLit && mouseIsOver) // only revive if mouse is over the campfire when released
        {
            ember.GetComponent<PlayerHealth>().revive(fireLocation);
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

            ntManager.SetNewNotification("Campfire lit! 1 Point gained!");
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

            //decrement the player points
            ember.GetComponent<PlayerPoints>().decrementPoints();

            // change collider size
            GetComponent<BoxCollider2D>().size = unLitBoxColSize;
            GetComponent<BoxCollider2D>().offset = unLitBoxColOffset;
            // disable upward wind effect
            GetComponent<AreaEffector2D>().enabled = false;

            //stop the fire sound and play the snuff fire sound
            GetComponent<AudioSource>().mute = true;
            AudioManager.PlaySound("snuffFire");
        }
    }
}
