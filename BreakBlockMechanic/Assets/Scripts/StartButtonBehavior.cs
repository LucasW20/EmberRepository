using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonBehavior : MonoBehaviour
{
    int n;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnButtonPress()
    {
        n++;
        Debug.Log("Start button clicked " + n + " times.");
    }
}
