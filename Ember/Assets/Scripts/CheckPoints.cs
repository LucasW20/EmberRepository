using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    bool[] checkPoints = new bool[7] {true, false, false, false, false, false, false };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCheckPoint(int scneneIndex)
    {
        checkPoints[scneneIndex - 1] = true;
    }

    public bool checkCheckPoint(int sceneIndex)
    {
        return checkPoints[sceneIndex - 1];
    }
}
