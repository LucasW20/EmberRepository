using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonBehavior : MonoBehaviour
{
    int n;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OnButtonPress() {
        SceneManager.LoadScene(1);
        n++;
        Debug.Log("Start button clicked " + n + " times.");
    }
}
