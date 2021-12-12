using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/***
 * Handles the unique behaviour of the credits level.
 * @author Lucas_C_Wright
 * @start 11-29-21
 * @version 12-12-21
 */
public class CreditsScript : MonoBehaviour {
    [SerializeField] GameObject WindForce;
    private PlayerHealth emberHealth;
    private AudioSource bgmTrack;
    private GameObject ember;
    private Image fadeImage;
    private NotificationManager ntManager;
    private bool inactive = false;

    // Start is called before the first frame update
    void Start() {
        ember = GameObject.Find("Ember");
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        bgmTrack = GameObject.Find("SaveObject").GetComponent<AudioSource>();
        emberHealth = GameObject.Find("Ember").GetComponent<PlayerHealth>();
        ntManager = GameObject.Find("MainUICanvas").GetComponent<NotificationManager>();

        //freeze the player at the start and set it to the top of the level
        emberHealth.revive(new Vector2(0,35));
        emberHealth.FreezeHealth();
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        ember.GetComponent<Transform>().position = new Vector3(0, 35, 0);
        StartCoroutine(StartCreditsCoroutine());
    }

    // Update is called once per frame
    void Update() {
        //when the spacebar is pressed start the closing coroutine that exits the game
        if (inactive && Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(CloseCreditsCoroutine());
        }
        if (!inactive && GameObject.Find("CreditsFire").GetComponent<Campfire>().isFireLit()) {
            inactive = true;
            WindForce.SetActive(true);
            ntManager.SetNewNotification("Press [SPACE] to end the game!");
        }
    }


    private IEnumerator StartCreditsCoroutine() {
        yield return new WaitForSeconds(5f);
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        ember.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -0.1f));
    }

    //special coroutine to end the credits level instead of the one in SceneChange
    private IEnumerator CloseCreditsCoroutine() {
        float fadeTime = 2.5f;
        float time = 0;
        float volStart = bgmTrack.volume;

        //fade out the image and the music
        while (time < fadeTime) {
            //update the time
            time += Time.unscaledDeltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(0f, 1f, time / fadeTime));
            bgmTrack.volume = Mathf.Lerp(volStart, 0f, time / fadeTime);


            yield return null; //finish the loop
        }

        //wait for a few more seconds for the fade to take place
        yield return new WaitForSeconds(3);
        
        //change the scene to the main menu
        SceneManager.LoadScene(0);
    }
}
