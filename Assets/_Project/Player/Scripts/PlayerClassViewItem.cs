using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerClassViewItem : MonoBehaviour, ISelectHandler
{
    [SerializeField] private TextMeshProUGUI txtClassName;
    [SerializeField] private Button btnClass;

    private UnityAction OnSelection;

    public Button BtnClass { get => btnClass; set => btnClass = value; }

    public void Fill(string className, UnityAction onButtonClick, UnityAction onSelect)
    {
        txtClassName.text = className;
        btnClass.onClick.AddListener(onButtonClick);
        OnSelection = onSelect;
    }
    public void OnSelect(BaseEventData eventData)
    {
        OnSelection?.Invoke();
    }

}
