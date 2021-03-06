using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuOpen : MonoBehaviour
{
    private bool isOpen = false;
    [SerializeField] bool canOpenWithKey = true;
    [SerializeField] GameObject menu;
    GameObject windForceM1;
    // Start is called before the first frame update
    void Start()
    {
        windForceM1 = GameObject.Find("WindForceM1");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canOpenWithKey)
        {
            if (isOpen) 
            {
                toggleMenu(false);
            }
            else if (!isOpen)
            {
                toggleMenu(true);
            }
        }
    }

    public void toggleMenu(bool tf)
    {
        menu.SetActive(tf);
        windForceM1.GetComponent<Windforce>().toggleWindForce(!tf);
        isOpen = tf;
    }
}
