using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setHealth(float nHealth)
    {
        slider.value = nHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

    public void setMaxHealth(float nMaxHealth)
    {
        slider.maxValue = nMaxHealth;
        slider.value = nMaxHealth;

        fill.color = gradient.Evaluate(1f);
    }
}
