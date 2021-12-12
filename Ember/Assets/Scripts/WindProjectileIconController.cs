using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindProjectileIconController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Displays the visual for the wind projectile icon
    public void displayWindIcon()
    {
        GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
