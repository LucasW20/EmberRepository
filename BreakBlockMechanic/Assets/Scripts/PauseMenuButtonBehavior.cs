using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtonBehavior : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] PlayerPoints playerPoints;
    [SerializeField] PlayerLives playerLives;
    PassingScene passingScene;
    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        passingScene = GameObject.Find("SaveObject").GetComponent<PassingScene>();
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

    public void toggleMenu(bool tf)
    {
        menu.SetActive(tf);
        Debug.Log(tf);
    }
}
