using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    
    private int maxValue;

    public void Initialize(int max, bool full = false)
    {
        maxValue = max;
        SetProgress(full ? maxValue : 0);

    }

    public void SetProgress(int currentValue)
    {
        float fillAmount = Mathf.Clamp01((float)currentValue / maxValue);
        fill.fillAmount = fillAmount;
    }
}
