using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingScene : MonoBehaviour {
    [HideInInspector] public int passingTotalPoints = 0;
    [HideInInspector] public int passingCurrPoints = 0;
    int passingMaxLives = 2;
    int passingFrostResist = 0;
    int passingTotalJumps = 3;
    float passingShieldDuration = 2;

    private bool[] purchasedAbilities = new bool[12] { false, false, false, false, false, false, false, false, false, false, false, false };
    private bool[] buttonIsPressedArray = new bool[12] { false, false, false, false, false, false, false, false, false, false, false, false };
    private bool[] campfireArray = new bool[50];

    private bool windProjectileIconDisplay = false;
    private bool dashCountIconDisplay = false;
    private bool shieldCountIconDisplay = false;

    private void Start()
    {
        //passingMaxLives = GameObject.Find("Ember").GetComponent<PlayerLives>().getMaxLives();
        Debug.Log(passingMaxLives);
        //passingFrostResist = GameObject.Find("Ember").GetComponent<PlayerHealth>().getFrostResist();
        //passingTotalJumps = GameObject.Find("Ember").GetComponent<LongJump>().getTotalJumps();
        //passingShieldDuration = GameObject.Find("Ember").GetComponent<BubbleShieldBehavior>().getShieldDuration();
    }
    public bool checkCampfireLit(int nIndex) { return campfireArray[nIndex]; }
    public void toggleCampfireLit(int nIndex, bool tf) { campfireArray[nIndex] = tf; }

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

    public void togglePurchasedAbility(int index, bool tf)
    {
        purchasedAbilities[index] = tf;
    }

    public bool getPurchasedAbility(int index)
    {
        return purchasedAbilities[index];
    }

    public void toggleButtonIsPressed(int index, bool tf)
    {
        buttonIsPressedArray[index] = tf;
    }

    public bool getButtonIsPressed(int index)
    {
        return buttonIsPressedArray[index];
    }

    public void displayWindProjectileIcon() { windProjectileIconDisplay = true; }
    public bool getWindProjectileIconDisplay() { return windProjectileIconDisplay; }
    public void displayDashCountIcon() { dashCountIconDisplay = true; }
    public bool getDashCountIconDisplay() { return dashCountIconDisplay; }

    public void displayShieldCountIcon() { shieldCountIconDisplay = true; }
    public bool getShieldCountIconDisplay() { return shieldCountIconDisplay; }
}
