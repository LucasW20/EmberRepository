using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingScene : MonoBehaviour {
    [HideInInspector] public int passingTotalPoints = 0;
    [HideInInspector] public int passingCurrPoints = 0;
    int passingMaxLives = 2;
    [HideInInspector] public int passingFrostResist;
    [HideInInspector] public int passingTotalJumps;
    [HideInInspector] public float passingShieldDuration;

    private void Start()
    {
        //passingMaxLives = GameObject.Find("Ember").GetComponent<PlayerLives>().getMaxLives();
        Debug.Log(passingMaxLives);
        passingFrostResist = GameObject.Find("Ember").GetComponent<PlayerHealth>().getFrostResist();
        passingTotalJumps = GameObject.Find("Ember").GetComponent<LongJump>().getTotalJumps();
        passingShieldDuration = GameObject.Find("Ember").GetComponent<BubbleShieldBehavior>().getShieldDuration();
    }
    public void passCurrPoints(int nPoints) { passingCurrPoints += nPoints; }
    public void passTotalPoints(int nPoints) { passingTotalPoints += nPoints; }
    public int getCurrPoints() { return passingCurrPoints; }
    public int getTotalPoints() { return passingTotalPoints; }

    public void passMaxLives(int nLives) { passingMaxLives += nLives; }
    public int getMaxLives() { return passingMaxLives; }

    public void passFrostResist(int nResist) { passingFrostResist += nResist; }
    public int getFrostResist() { return passingFrostResist; }

    public void passTotalJumps(int nJumps) { passingTotalJumps += nJumps; }
    public int getTotalJumps() { return passingTotalJumps; }

    public void passShieldDuration(float nDuration) { passingShieldDuration += nDuration; }
    public float getShieldDuration() { return passingShieldDuration; }
}
