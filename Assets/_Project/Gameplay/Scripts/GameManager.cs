using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Match Settings")]
    [SerializeField] private int pointsToWin = 25;
    [SerializeField] private float matchTime;
    [Header("Bots settings")]
    [Range(0f, 5f), SerializeField] private int alliesBotsCount = 0;
    [Range(0f, 5f), SerializeField] private int axisBotsCount = 0;
    [Space]
    [SerializeField] private BotController alliesBot;
    [SerializeField] private BotController axisBot;
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
    [SerializeField] private Button btnBack;

    private float currentMatchTime = 0;

    private List<PlayerClassView> alliesTeam = new List<PlayerClassView>();
    private List<PlayerClassView> axisTeam = new List<PlayerClassView>();

    public static int setAlliesCount = 0;
    public static int setAxisCount = 0;

    private void Start()
    {
        alliesBotsCount = setAlliesCount;
        axisBotsCount = setAxisCount;

        alliesPoints = 0;
        axisPoints = 0;

        txtAlliesPoints.text = alliesPoints.ToString();
        txtAxisPoints.text = axisPoints.ToString();
        currentMatchTime = matchTime;

        SpawnAlliesBots();
        SpawnAxisBots();

        btnBack.onClick.AddListener(BtnBackClick);

    }

    private void SpawnAlliesBots()
    {
        for (int i = 0; i < alliesBotsCount; i++)
        {
            var bot = Instantiate(alliesBot);
            bot.Team = PlayerTeam.Allies;
            bot.OnPlayerDeath += AddAxisPoints;
        }
    }

    private void SpawnAxisBots()
    {
        for (int i = 0; i < axisBotsCount; i++)
        {
            var bot = Instantiate(axisBot);
            bot.Team = PlayerTeam.Axis;
            bot.OnPlayerDeath += AddAlliesPoints;
        }
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

        EventSystem.current.SetSelectedGameObject(btnBack.gameObject);
        
    }

    private void BtnBackClick()
    {
        SceneManager.LoadScene(MainMenuController.sceneName);
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
