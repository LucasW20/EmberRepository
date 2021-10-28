using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingScene : MonoBehaviour {
    [HideInInspector] public int passingPoints = 0;
    public void passPoints(int nPoints) { passingPoints += nPoints; }
    public int getPoints() { return passingPoints; }
}
