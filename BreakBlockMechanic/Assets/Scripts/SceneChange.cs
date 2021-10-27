using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneChange : MonoBehaviour {
    [SerializeField] public TextMeshProUGUI titleObject;
    [SerializeField] public string sceneTitle;
    [SerializeField] public int requiredPoints;   //the total amount of points needed to unlock the next area. set/change in unity
    [SerializeField] public int nextScene;     //the next scene playing. set/change in unity
    [SerializeField] public float fadeTime = 2f;
    private AudioSource bgmSource;
    private bool opened = false;
    private Image fadeImage;

    void Start() {
        bgmSource = GameObject.Find("SaveObject").GetComponent<AudioSource>();
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        StartCoroutine(SceneOpenCoroutine());
    }

    // Update is called once per frame
    void Update() {
        //if the amount of points required to move on has been hit then open the path.
        if (GameObject.Find("Ember").GetComponent<PlayerPoints>().getPoints() >= requiredPoints && !opened) {
            //change the collider to a trigger collider
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            opened = true;
            Debug.Log("The Pathway is open!");
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            //set the camera back to the whole map
            Camera.main.GetComponent<FollowPlayer>().setGoToCenter(true);
            Camera.main.GetComponent<FollowPlayer>().setTrackPlayer(false);

            //TODO make it so the player doesn't loose health and fall

            //start the closing coroutine
            StartCoroutine(SceneCloseCoroutine());
        }
    }

    IEnumerator SceneOpenCoroutine() {
        //set base time
        float time = 0;

        //set the alpha to full and set the text for the title
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        titleObject.text = sceneTitle;

        //fade in the title text
        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;
            titleObject.color = new Color(titleObject.color.r, titleObject.color.g, titleObject.color.b, Mathf.Lerp(0f, 1f, time / fadeTime));
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
            yield return null;                  //finish the loop
        }
    }

    IEnumerator SceneCloseCoroutine() {
        //Wait for the zoom out to complete
        yield return new WaitForSeconds(5);

        //set base time and the starting volume
        float time = 0;
        float startVol = bgmSource.volume;

        //fade out the image and the music
        while (time < fadeTime) {
            //update the time
            time += Time.unscaledDeltaTime;

            //fade
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(0f, 1f, time / fadeTime));
            bgmSource.volume = Mathf.Lerp(startVol, 0f, time / fadeTime);

            //finish the loop
            yield return null; 
        }

        //wait for a few more seconds for the fade to take place
        yield return new WaitForSeconds(3);

        //save the points the player has
        DontDestroyOnLoad(GameObject.Find("SaveObject"));

        //change the scene
        SceneManager.LoadScene(nextScene);
    }
}
