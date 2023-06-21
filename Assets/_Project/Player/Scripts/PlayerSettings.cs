using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [Space]
    [SerializeField] private Button btnTeam;
    [SerializeField] private TextMeshProUGUI txtTeam;

    private PlayerTeam currentTeam;

    private void Start()
    {
        btnTeam.onClick.AddListener(BtnteamClick);
    }

    private void BtnteamClick()
    {
        currentTeam++;
        if((int)currentTeam > 2) 
        {
            currentTeam = 0;
        }

        txtTeam.text = currentTeam.ToString().FirstCharacterToUpper();
    }
}
