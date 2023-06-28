using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Match Settings")]
    [SerializeField] private int pointsToWin = 25;
    [SerializeField] private float matchTime;
    [Header("Score")]
    [SerializeField] private int alliesPoints = 0;
    [SerializeField] private int axisPoints = 0;
    [Header("Score screen")]
    [SerializeField] private TextMeshProUGUI txtAlliesPoints;
    [SerializeField] private TextMeshProUGUI txtAxisPoints;
    [SerializeField] private TextMeshProUGUI txtTime;
    [Header("End Screen")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI txtVictory;

    private float currentMatchTime = 0;

    private List<PlayerClassView> alliesTeam = new List<PlayerClassView>();
    private List<PlayerClassView> axisTeam = new List<PlayerClassView>();

    private void Start()
    {
        alliesPoints = 0;
        txtAlliesPoints.text = alliesPoints.ToString();
        axisPoints = 0;
        txtAxisPoints.text = axisPoints.ToString();
        currentMatchTime = matchTime;
    }

    public void OnPlayerEnter(PlayerInput player)
    {
        AssignTeamToPlayer(player.GetComponent<PlayerClassView>());
    }

    private void AssignTeamToPlayer(PlayerClassView player)
    {
        if(alliesTeam.Count == 0)
        {
            alliesTeam.Add(player);
            player.Team = PlayerTeam.Allies;
            player.OnPlayerDeath += AddAxisPoints;
            return;
        }

        if(axisTeam.Count == 0)
        {
            axisTeam.Add(player);
            player.Team = PlayerTeam.Axis;
            player.OnPlayerDeath += AddAlliesPoints;
            return;
        }

        if(alliesTeam.Count <= axisTeam.Count)
        {
            alliesTeam.Add(player);
            player.Team = PlayerTeam.Allies;
            player.OnPlayerDeath += AddAxisPoints;
        }
        else
        {
            axisTeam.Add(player);
            player.Team = PlayerTeam.Axis;
            player.OnPlayerDeath += AddAlliesPoints;
        }
    }

    private void EndMatch()
    {
        endScreen.SetActive(true);

        if(alliesPoints > axisPoints)
        {
            txtVictory.text = "The Allies won the battle!";
            return;
        }
        if(axisPoints > alliesPoints)
        {
            txtVictory.text = "The Axis won the battle!";
            return;
        }

        txtVictory.text = "The match was a tie!";
        
    }

    private void Update()
    {
        currentMatchTime -= Time.deltaTime;
        float minutes = Mathf.Floor(currentMatchTime / 60);
        float seconds = currentMatchTime % 60;

        txtTime.text = $"{minutes:00}:{seconds:00}";

        if(currentMatchTime <= 0)
        {
            EndMatch();
        }
    }

    private void AddAlliesPoints()
    {
        alliesPoints++;
        txtAlliesPoints.text = alliesPoints.ToString();
        if(alliesPoints > pointsToWin)
        {
            EndMatch();
        }
    }

    private void AddAxisPoints()
    {
        axisPoints++;
        txtAxisPoints.text = axisPoints.ToString();
        if (alliesPoints > pointsToWin)
        {
            EndMatch();
        }
    }
}
