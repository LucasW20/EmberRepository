using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingScene : MonoBehaviour {

    [SerializeField] public int passingPoints = 0;

    // Start is called before the first frame update
    void Start() {

    }

    public void passPoints(int nPoints) {
        passingPoints += nPoints;
    }

    public int getPoints() {
        return passingPoints;
    }

}
