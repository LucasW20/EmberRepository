using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCounter : MonoBehaviour
{
    Slider slider;
    int dashCount;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        dashCount = GameObject.Find("SaveObject").GetComponent<PassingScene>().getTotalJumps();

        if(dashCount < 3)
        {
            dashCount = 3;
        }
        if(GameObject.Find("SaveObject").GetComponent<PassingScene>().getDashCountIconDisplay())
        {
            displayDashCounter();
        }

        slider.maxValue = dashCount;
        slider.value = dashCount;
    }

    // decreases the dash counter by 1
    public void useDash()
    {
        slider.value -= 1;
    }

    // Increases the max number of dashes in the counter
    public void increaseDashes()
    {
        slider.maxValue += 1;
        slider.value += 1;
    }

    // Call this to toggle the dash Counter's image
    public void displayDashCounter()
    {
        GameObject.Find("DashIcon").GetComponent<Image>().enabled = true;
        GameObject.Find("DashBorder").GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
