using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldCounter : MonoBehaviour
{
    Slider slider;
    int shieldCount;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        shieldCount = 4;

        if (GameObject.Find("SaveObject").GetComponent<PassingScene>().getShieldCountIconDisplay())
        {
            displayShieldCounter();
        }

        slider.maxValue = shieldCount;
        slider.value = shieldCount;
    }

    // decreases the shield counter by 1
    public void useShield()
    {
        slider.value -= 1;
    }

    // Call this to toggle the shield Counter's image
    public void displayShieldCounter()
    {
        GameObject.Find("ShieldIcon").GetComponent<Image>().enabled = true;
        GameObject.Find("ShieldBorder").GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
