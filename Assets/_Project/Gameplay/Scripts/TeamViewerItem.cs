using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeamViewerItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;

    public void Fill(string name)
    {
        txtName.text = name;
    }
}
