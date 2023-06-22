using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Pre match")] 
    [SerializeField] private TeamViewer teamViewer;
    [SerializeField] private Button btnStart;
    [SerializeField] private GameObject preMatchObj;
    [Header("On Match")]
    [SerializeField] private GameObject onMatchObj;
    [SerializeField] private int alliesPoints = 0;
    [SerializeField] private int axisPoints = 0;
    [SerializeField] private ScoreViewer scoreViewer;
    
    [Space]
    [SerializeField] private List<PlayerInput> alliesTeam = new List<PlayerInput>();
    [SerializeField] private List<PlayerInput> axisTeam = new List<PlayerInput>();
    
    public static bool IsPlayingMatch = false;

    private void Start()
    {
        btnStart.onClick.AddListener(StartMatch);
    }

    public void OnPlayerEnter(PlayerInput player)
    {
        AssignTeamToPlayer(player);
    }

    private void AssignTeamToPlayer(PlayerInput player)
    {
        if(alliesTeam.Count == 0)
        {
            alliesTeam.Add(player);
            teamViewer.FillAllies(alliesTeam);
        }
        else if(axisTeam.Count == 0)
        {
            axisTeam.Add(player);
            teamViewer.FillAxis(alliesTeam);
        }
        else if(alliesTeam.Count <= axisTeam.Count) 
        {
            alliesTeam.Add(player);
            teamViewer.FillAllies(alliesTeam);
        }
        else
        {
            axisTeam.Add(player);
            teamViewer.FillAxis(alliesTeam);
        }
    }

    private void StartMatch()
    {
        //TODO custom spawns for each team
        IsPlayingMatch = true;
        preMatchObj.SetActive(false);
        onMatchObj.SetActive(true);
        alliesPoints = 0;
        axisPoints = 0;
        
        scoreViewer.UpdateScore(alliesPoints, axisPoints);
    }
}
