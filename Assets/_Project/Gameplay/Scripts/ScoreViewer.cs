using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtAlliesPoints;
    [SerializeField] private TextMeshProUGUI txtAxisPoints;
    [SerializeField] private TextMeshProUGUI txtTimer;

    public void UpdateTimer(string time)
    {
        txtTimer.text = time;
    }

    public void UpdateScore(int allies, int axis)
    {
        txtAlliesPoints.text = allies.ToString("00");
        txtAxisPoints.text = axis.ToString("00");
    }
}
