    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtonBehavior : MonoBehaviour
{
    [SerializeField] GameObject menu;
    GameObject ember;
    PlayerPoints playerPoints;
    PlayerLives playerLives;
    PlayerHealth playerHealth;
    PassingScene passingScene;
    LongJump longJump;
    BubbleShieldBehavior bubbleShieldBehavior;
    //private bool isPressed = false;
    [SerializeField] int aIndex;

    SceneChange sceneChange;
    CheckPoints checkPoints;

    // Start is called before the first frame update
    void Start()
    {
        passingScene = GameObject.Find("SaveObject").GetComponent<PassingScene>();
        ember = GameObject.Find("Ember");
        playerPoints = ember.GetComponent<PlayerPoints>();
        playerLives = ember.GetComponent<PlayerLives>();
        playerHealth = ember.GetComponent<PlayerHealth>();
        longJump = ember.GetComponent<LongJump>();
        bubbleShieldBehavior = ember.GetComponent<BubbleShieldBehavior>();
        sceneChange = GameObject.Find("ExitObject").GetComponent<SceneChange>();
        checkPoints = GameObject.Find("SaveObject").GetComponent<CheckPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void purchaseAbility(int nCost)
    {
        bool tempBool = false;
        if (!passingScene.getButtonIsPressed(aIndex) && playerPoints.getCurrentPoints() >= nCost && !passingScene.getPurchasedAbility(aIndex))
        {
            if(aIndex % 3 == 2 && passingScene.getPurchasedAbility(aIndex - 1) && passingScene.getPurchasedAbility(aIndex - 2))
            {
                tempBool = true;
                Debug.Log("First If");
            }

            if (aIndex % 3 == 1 && passingScene.getPurchasedAbility(aIndex - 1))
            {
                tempBool = true;
                Debug.Log("Second If");
            }

            if (aIndex % 3 == 0)
            {
                tempBool = true;
                Debug.Log("Third if");
            }
            // decrement the current player points, but not the total points, nCost times
            if (tempBool)
            {
                for (int i = 0; i < nCost; i++)
                {
                    playerPoints.decrementPoints(false);
                }
                passingScene.toggleButtonIsPressed(aIndex, true);
            }
            else
            {
                Debug.Log("Cannot Purchase");
            }
        }
        else
        {
            Debug.Log("Not Enough Points");
        }
    }

    public void loadLevel(int sceneNum)
    {
        if (checkPoints.checkCheckPoint(sceneNum))
        {
            sceneChange.loadNewScene(sceneNum, 0);
        }
    }

    public void livesIncrease(int n)
    {
        if (passingScene.getButtonIsPressed(aIndex) && !passingScene.getPurchasedAbility(aIndex))
        {
            playerLives.adjustMaxLives(n);
            passingScene.passMaxLives(n);
            //isPressed = true;
            passingScene.togglePurchasedAbility(aIndex, true);
        }
        
    }

    public void frostResistIncrease(int n)
    {
        if (passingScene.getButtonIsPressed(aIndex) && !passingScene.getPurchasedAbility(aIndex))
        {
            playerHealth.adjustFrostReist(n);
            passingScene.passFrostResist(n);
            //isPressed = true;
            passingScene.togglePurchasedAbility(aIndex, true);
            Debug.Log("frost up");
        }
    }

    public void totalJumpsIncrease(int n)
    {
        if (passingScene.getButtonIsPressed(aIndex) && !passingScene.getPurchasedAbility(aIndex))
        {
            longJump.adjustTotalJumps(n);
            passingScene.passTotalJumps(n);
            //isPressed = true;
            passingScene.togglePurchasedAbility(aIndex, true);
        }
    }

    public void shieldDurationIncrease(float n)
    {
        if (passingScene.getButtonIsPressed(aIndex) && !passingScene.getPurchasedAbility(aIndex))
        {
            bubbleShieldBehavior.adjustShieldDuration(n);
            passingScene.passShieldDuration(n);
            //isPressed = true;
            passingScene.togglePurchasedAbility(aIndex, true);
        }
    }

    public void toggleMenu(bool tf)
    {
        menu.SetActive(tf);
        Debug.Log(tf);
    }
}
