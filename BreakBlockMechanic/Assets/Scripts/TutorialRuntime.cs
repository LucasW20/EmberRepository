using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/***
 * Handles the runtime of the tutorial level
 * @author Lucas_C_Wright
 * @start 11-11-2021
 * @version 11-16-2021
 */
public class TutorialRuntime : MonoBehaviour {

    [SerializeField] TextMeshProUGUI tutorialText; //object for the text in the canvas. set in unity
    private bool running;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(StartTurorialCoroutine());
    }

    // Update is called once per frame
    void Update() {
        
    }

    private IEnumerator StartTurorialCoroutine() {
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator AfterFirstFireCoroutine() {
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator FinalTutorialCoroutine() {
        yield return new WaitForSeconds(1f);
    }
}
