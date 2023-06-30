using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalPlayController : MonoBehaviour
{
    public static readonly string sceneName = "LocalPlay";

    [SerializeField] private MapData[] maps;
    [Space]
    [SerializeField] private TMP_Dropdown dropdownMaps;
    [SerializeField] private Slider alliesBotsAmount;
    [SerializeField] private Slider axisBotsAmount;
    [SerializeField] private TextMeshProUGUI txtAlliesBot;
    [SerializeField] private TextMeshProUGUI txtAxisBot;
    [Space]
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnPlay;
    [Space]
    [SerializeField] private GameObject firstSelected;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);

        List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();

        dropdownMaps.ClearOptions();

        foreach (var map in maps)
        {
            list.Add(new TMP_Dropdown.OptionData(map.MapScene, map.mapIcon));
        }

        dropdownMaps.AddOptions(list);
        alliesBotsAmount.onValueChanged.AddListener(OnAlliesBotChange);
        axisBotsAmount.onValueChanged.AddListener(OnAxisBotChange);
        btnBack.onClick.AddListener(BtnBackClick);
        btnPlay.onClick.AddListener(BtnPlayClick);
    }

    private void BtnBackClick()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    private void BtnPlayClick()
    {
        SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.LoadScene(maps[dropdownMaps.value].MapScene);
    }

    private void OnAlliesBotChange(float arg0)
    {
        txtAlliesBot.text = arg0.ToString();
        GameManager.setAlliesCount = (int)arg0;
    }

    private void OnAxisBotChange(float arg0)
    {
        txtAxisBot.text = arg0.ToString();
        GameManager.setAxisCount = (int)arg0;
    }
}

