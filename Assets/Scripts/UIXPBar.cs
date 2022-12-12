using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIXPBar : MonoBehaviour
{
    
    public Slider slider;

    public void SetMaxXP(int maxXP)
    {
        slider.maxValue = maxXP;
    }

    public void SetXP(int currentXP)
    {
        slider.value = currentXP;
    }
}
