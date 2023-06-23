using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerClassViewItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtClassName;
    [SerializeField] private Button btnClass;

    public void Fill(string className, UnityAction onButtonClick)
    {
        txtClassName.text = className;
        btnClass.onClick.AddListener(onButtonClick);
    }
}
