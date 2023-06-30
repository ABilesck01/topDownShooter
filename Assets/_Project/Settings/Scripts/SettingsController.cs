using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static readonly string sceneName = "Options";

    [SerializeField] private GameObject firstSelected;
    [Header("Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [Header("Buttons")]
    [SerializeField] private TMP_Dropdown dropdownQuality;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Button btnBack;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
        dropdownQuality.onValueChanged.AddListener(dropdownQualityValueChanged);
        masterVolumeSlider.onValueChanged.AddListener(masterVolumeSliderValueChanged);
        btnBack.onClick.AddListener(BtnBackClick);
    }


    private void dropdownQualityValueChanged(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }
    private void masterVolumeSliderValueChanged(float value)
    {
        audioMixer.SetFloat("master", value);
    }

    private void BtnBackClick()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
