using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireTracker : MonoBehaviour
{
    int numFiresLit = 0;
    LightingBehaviour lightController;
    // Start is called before the first frame update
    void Start()
    {
        lightController = GameObject.Find("Ember").GetComponent<LightingBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getNumfiresLit()
    {
        return numFiresLit;
    }
    public void increaseNumFiresLit()
    {
        numFiresLit++;
        lightController.increaseLighting();
    }
    public void decreaseNumFireLit()
    {
        if (numFiresLit > 0)
        {
            numFiresLit--;
            lightController.decreaseLighting();
        }
    }
}
