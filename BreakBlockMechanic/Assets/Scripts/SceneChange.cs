using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

/***
 * Handles the changing of scenes in the game along with the fade in and fade out mechanics
 * @author Lucas_C_Wright
 * @start 10-18-2021
 * @version 10-28-2021
 */
public class SceneChange : MonoBehaviour {
    [SerializeField] public TextMeshProUGUI titleObject;
    [SerializeField] public string sceneTitle;
    [SerializeField] public int requiredPoints;   //the total amount of points needed to unlock the next area. set/change in unity
    [SerializeField] public int nextScene;     //the next scene playing. set/change in unity
    [SerializeField] public float fadeTime = 2f;
    [SerializeField] public float fadeLight = 1.9f;
    [SerializeField] public float baseLight = 0.5f;

    private AudioSource bgmTrack;
    private NotificationManager ntManager;
    private LightingBehaviour LightController;
    private GameObject ember;
    private bool opened = false;
    private Image fadeImage;
    private Light2D exitLight;

    // Start is called at the start
    void Start() {
        ntManager = GameObject.Find("MainUICanvas").GetComponent<NotificationManager>();
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        LightController = GameObject.Find("Ember").GetComponent<LightingBehaviour>();
        bgmTrack = GameObject.Find("SaveObject").GetComponent<AudioSource>();
        exitLight = gameObject.GetComponent<Light2D>();
        StartCoroutine(SceneOpenCoroutine());
    }

    // Update is called once per frame
    void Update() {
        //call every frame so when the scene changes it gets updates to the 'new' object
        ember = GameObject.Find("Ember");

        //if the amount of points required to move on has been hit then open the path.
        if (ember.GetComponent<PlayerPoints>().getPoints() >= requiredPoints && !opened) {
            //change the collider to a trigger collider
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            opened = true;
            
            //Display notification
            //Debug.Log("The Pathway is open!");
            ntManager.SetNewNotification("The fires have melted the pathway! Move to exit to continue.");
            StartCoroutine(LightExitCoroutine());
        }
    }

    // Called when the exit has been opened. Starts the coroutine and sets the map and player to the correct placements
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            //set the camera back to the whole map
            Camera.main.GetComponent<FollowPlayer>().setGoToCenter(true);
            Camera.main.GetComponent<FollowPlayer>().setTrackPlayer(false);

            //set player to not lose health and freeze when exit is triggered
            ember.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            ember.GetComponent<PlayerHealth>().FreezeHealth();

            //start the closing coroutine
            StartCoroutine(SceneCloseCoroutine(nextScene));
        }
    }

    // Coroutine for when a scene is first loaded
    IEnumerator SceneOpenCoroutine() {
        float time = 0;
        int currScene = SceneManager.GetActiveScene().buildIndex;

        //set the alpha to full and set the text for the title
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        titleObject.text = sceneTitle;

        if (currScene == 3) {
            bgmTrack.Stop();
            bgmTrack.PlayOneShot(Resources.Load<AudioClip>("Campfire"));
            bgmTrack.volume = 0f;
        }

        //fade in the title text
        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;
            titleObject.color = new Color(titleObject.color.r, titleObject.color.g, titleObject.color.b, Mathf.Lerp(0f, 1f, time / fadeTime));

            if (currScene == 3) {
                bgmTrack.volume = Mathf.Lerp(0f, 0.201f, time / fadeTime);
            }
            yield return null;
        }

        //wait for a little bit
        yield return new WaitForSeconds(2f);

        //fade out the image and title text together
        time = 0;   //reset time
        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;     //update the time
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(1f, 0f, time / fadeTime));
            titleObject.color = new Color(titleObject.color.r, titleObject.color.g, titleObject.color.b, Mathf.Lerp(1f, 0f, time / fadeTime));
            yield return null; //finish the loop
        }
    }

    // Coroutine used for when a scene is being closed. Fades out of scene. 
    IEnumerator SceneCloseCoroutine(int sceneNum) {
        //Wait for the zoom out to complete
        yield return new WaitForSeconds(5);
        float time = 0;
        float volStart = bgmTrack.volume;

        //fade out the image and the music
        while (time < fadeTime) {
            //update the time
            time += Time.unscaledDeltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(0f, 1f, time / fadeTime));

            if (sceneNum == 3) {
                bgmTrack.volume = Mathf.Lerp(volStart, 0f, time / fadeTime);
            }

            yield return null; //finish the loop
        }

        //wait for a few more seconds for the fade to take place
        yield return new WaitForSeconds(3);

        //save the points the player has. This also saves the bgm music. Then change the scene
        DontDestroyOnLoad(GameObject.Find("SaveObject"));
        SceneManager.LoadScene(sceneNum);

        //reset the lighting of the ember
        LightController.resetLight();
    }

    IEnumerator LightExitCoroutine() {
        float time = 0;
        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;
            exitLight.intensity = Mathf.Lerp(0, fadeLight, time / fadeTime);
            yield return null;
        }

        time = 0; //reset time
        while (true) {
            yield return new WaitForSeconds(0.5f);

            //fade out but not to 0
            while (time < fadeTime) {
                time += Time.unscaledDeltaTime;
                exitLight.intensity = Mathf.Lerp(fadeLight, baseLight, time / fadeTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);  //wait for a little bit
            time = 0; //reset time

            //fade in
            while (time < fadeTime) {
                time += Time.unscaledDeltaTime;
                exitLight.intensity = Mathf.Lerp(baseLight, fadeLight, time / fadeTime);
                yield return null;
            }

            time = 0; //reset time
        }
    }
}
