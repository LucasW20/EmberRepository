using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public int requiredPoints;   //the total amount of points needed to unlock the next area. set/change in unity
    public int nextScene;     //the next scene playing. set/change in unity
    bool opened = false;

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

            //wait for a few seconds
            StartCoroutine(SceneChangeCoroutine());

        }
    }

    IEnumerator SceneChangeCoroutine() {

        float time = 0;
        float fadeTime = 4f;
        SpriteRenderer fadeImage = GameObject.Find("ScriptHolder").GetComponent<SpriteRenderer>();
        while (time < fadeTime) {
            Debug.Log("aplha change");
            time += Time.unscaledDeltaTime;     //update the time
            fadeImage.color = new Color(fadeImage.color.r,    //update the color
                                  fadeImage.color.g,
                                  fadeImage.color.b,
                                  Mathf.Lerp(0f, 1f, time / fadeTime));
            yield return null;//finish the loop
        }


        //yield on a new YieldInstruction that waits for 5 seconds.
        //yield return new WaitForSeconds(5);

        //save the points the player has
        DontDestroyOnLoad(GameObject.Find("SaveObject"));


        

        //change the scene
        SceneManager.LoadScene(nextScene);
    }
}
