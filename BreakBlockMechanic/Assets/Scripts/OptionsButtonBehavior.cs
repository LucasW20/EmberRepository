using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButtonBehavior : MonoBehaviour
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
