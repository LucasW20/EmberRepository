using UnityEngine;
using UnityEngine.SceneManagement;

/***
 * Handles the behaviour of the start tutorial button
 * @author Lucas_C_Wright
 * @start 11-29-21
 * @version 11-29-21
 */
public class TutorialButtonBehaviour : MonoBehaviour {
    //loads the tutorial scene when the button is pressed
    public void OnButtonClick() {
        SceneManager.LoadScene(8);
    }
}
