using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider hpSlider;

    public void SetMaxValue(float maxValue)
        => hpSlider.maxValue = maxValue;

    public void SetValue(float value)
        => hpSlider.value = value;
}
