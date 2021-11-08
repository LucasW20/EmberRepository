using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsTextUpdater : MonoBehaviour
{
    private GameObject ember;
    // Start is called before the first frame update
    void Start()
    {
        ember = GameObject.Find("Ember");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = ember.GetComponent<PlayerPoints>().getCurrentPoints().ToString();
    }
}
