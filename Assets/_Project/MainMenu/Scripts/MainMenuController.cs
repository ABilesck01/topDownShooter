using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static readonly string sceneName = "MainMenu";

    [SerializeField] private GameObject firstSelected;
    [Header("Buttons")]
    [SerializeField] private Button btnLocalPlay;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnQuit;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);

        btnSettings.onClick.AddListener(BtnSettingsClick);
        btnQuit.onClick.AddListener(BtnQuitClick);
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    private void BtnSettingsClick()
    {
        SceneManager.LoadScene(SettingsController.sceneName, LoadSceneMode.Additive);
    }

    private void BtnQuitClick()
    {
        Application.Quit();
    }
}
