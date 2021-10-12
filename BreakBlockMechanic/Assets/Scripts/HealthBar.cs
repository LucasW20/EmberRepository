using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void setHealth(float nHealth)
    {
        slider.value = nHealth;
    }

    public void setMaxHealth(float nMaxHealth)
    {
        slider.maxValue = nMaxHealth;
        slider.value = nMaxHealth;
    }
}
