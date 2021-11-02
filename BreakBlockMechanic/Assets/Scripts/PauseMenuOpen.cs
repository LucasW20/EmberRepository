using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuOpen : MonoBehaviour
{
    private bool isOpen = false;
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
        if (Input.GetKeyDown("p"))
        {
            toggleMenu();
        }
    }

    public void toggleMenu()
    {
        menu.SetActive(!isOpen);
        windForceM1.GetComponent<Windforce>().toggleWindForce(isOpen);
        isOpen = !isOpen;

    }
}
