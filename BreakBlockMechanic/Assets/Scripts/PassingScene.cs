using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingScene : MonoBehaviour {
    [HideInInspector] public int passingPoints = 0;
    [HideInInspector] public int passingMaxLives;
    [HideInInspector] public int passingFrostResist;
    [HideInInspector] public int passingTotalJumps;

    private void Start()
    {
        passingMaxLives = GameObject.Find("Ember").GetComponent<PlayerLives>().getMaxLives();
        passingFrostResist = GameObject.Find("Ember").GetComponent<PlayerHealth>().getFrostResist();
        passingTotalJumps = GameObject.Find("Ember").GetComponent<LongJump>().getTotalJumps();
    }
    public void passPoints(int nPoints) { passingPoints += nPoints; }
    public int getPoints() { return passingPoints; }

    public void passMaxLives(int nLives) { passingMaxLives += nLives; }
    public int getMaxLives() { return passingMaxLives; }

    public void passFrostResist(int nResist) { passingFrostResist += nResist; }
    public int getFrostResist() { return passingFrostResist; }

    public void passTotalJumps(int nJumps) { passingTotalJumps += nJumps; }
    public int getTotalJumps() { return passingTotalJumps; }
}
