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
    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        passingScene = GameObject.Find("SaveObject").GetComponent<PassingScene>();
        ember = GameObject.Find("Ember");
        playerPoints = ember.GetComponent<PlayerPoints>();
        playerLives = ember.GetComponent<PlayerLives>();
        playerHealth = ember.GetComponent<PlayerHealth>();
        longJump = ember.GetComponent<LongJump>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void purchaseAbility(int nCost)
    {
        if (!isPressed && playerPoints.getPoints() >= nCost)
        {
            playerPoints.adjustPoints(-nCost);
            isPressed = true;
        }
        else
        {
            Debug.Log("Not Enough Points");
        }
    }

    public void livesIncrease(int n)
    {
        playerLives.adjustMaxLives(n);
        passingScene.passMaxLives(n);
        
    }

    public void frostResistIncrease(int n)
    {
        playerHealth.adjustFrostReist(n);
        passingScene.passFrostResist(n);
    }

    public void totalJumpsIncrease(int n)
    {
        longJump.adjustTotalJumps(n);
        passingScene.passTotalJumps(n);
    }

    public void toggleMenu(bool tf)
    {
        menu.SetActive(tf);
        Debug.Log(tf);
    }
}
