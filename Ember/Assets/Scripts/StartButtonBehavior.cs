using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonBehavior : MonoBehaviour{
    //class variables
    [SerializeField] Image fadeImage;
    AudioSource musicPlayer;
    
    // Start is called before the first frame update
    void Start() {
        musicPlayer = GameObject.Find("Music").GetComponent<AudioSource>();
        fadeImage.enabled = false;
    }
    
    public void OnButtonPress() {
        fadeImage.enabled = true;
        StartCoroutine(StartGameCoroutine());
        
        //Debug.Log("Start button clicked " + n + " times.");
    }

    private IEnumerator StartGameCoroutine() {
        float fadeTime = 3.5f;
        float time = 0;
        float volStart = musicPlayer.volume;

        //fade out the image and the music
        while (time < fadeTime) {
            //update the time
            time += Time.unscaledDeltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(0f, 1f, time / fadeTime));
            musicPlayer.volume = Mathf.Lerp(volStart, 0f, time / fadeTime);

            yield return null; //finish the loop
        }

        SceneManager.LoadScene(1);
    }
}
