using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/***
 * Handles the unique behaviour of the credits level.
 * @author Lucas_C_Wright
 * @start 11-29-21
 * @version 12-07-21
 */
public class CreditsScript : MonoBehaviour {
    private PlayerHealth ember;
    private AudioSource bgmTrack;
    private Image fadeImage;

    // Start is called before the first frame update
    void Start() {
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        bgmTrack = GameObject.Find("SaveObject").GetComponent<AudioSource>();
        ember = GameObject.Find("Ember").GetComponent<PlayerHealth>();

        //freeze the player at the start and set it to the top of the level
        ember.FreezeHealth();
        ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        ember.GetComponent<Transform>().position = new Vector3(0, 40, 0);
    }

    // Update is called once per frame
    void Update() {
        //when the spacebar is pressed start the closing coroutine that exits the game
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(CloseCreditsCoroutine());
        }
    }


    private IEnumerator StartCreditsCoroutine() {
        yield return null;
    }

    //special coroutine to end the credits level instead of the one in SceneChange
    private IEnumerator CloseCreditsCoroutine() {
        float waitTime = 5;
        float fadeTime = 2.5f;
        float time = 0;
        float volStart = bgmTrack.volume;

        //Wait for the zoom out to complete
        yield return new WaitForSeconds(waitTime);

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
