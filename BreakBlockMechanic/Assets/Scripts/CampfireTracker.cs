using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireTracker : MonoBehaviour
{
    int numFiresLit = 0;
    // Start is called before the first frame update
    void Start()
    {

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
    }
    public void decreaseNumFireLit()
    {
        if (numFiresLit > 0)
        {
            numFiresLit--;
        }
    }
}
