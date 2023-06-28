using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static readonly string sceneName = "Settings";

    [SerializeField] private GameObject firstSelected;
    [Header("Buttons")]
    [SerializeField] private Button btnBack;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
        btnBack.onClick.AddListener(BtnBackClick);
    }

    private void BtnBackClick()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
