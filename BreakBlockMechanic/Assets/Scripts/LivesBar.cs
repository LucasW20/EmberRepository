using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesBar : MonoBehaviour
{

    public Slider slider;
    // Start is called before the first frame update
    public void setLives(int lives)
    {
        slider.value = lives;
    }

    // Update is called once per frame
    public void setMaxLives()
    {
        slider.value = 5;
    }
}
