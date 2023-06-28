using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    [SerializeField] private Image logo;
    [SerializeField] private TextMeshProUGUI warnningMessage;
    [SerializeField] private TextMeshProUGUI CanContinue;
    [SerializeField] private Button btnContinue;

    private bool showWarnning = false;

    private IEnumerator Start()
    {
        btnContinue.interactable = false;
        btnContinue.onClick.AddListener(() =>
        {
            if(showWarnning)
                SceneManager.LoadScene(MainMenuController.sceneName);
        });
        warnningMessage.DOFade(0, 0f);
        CanContinue.text = string.Empty;
        logo.DOFade(1, 0.15f);
        yield return new WaitForSeconds(1.5f);
        logo.DOFade(0, 0.15f);
        warnningMessage.DOFade(1, 0.15f);
        yield return new WaitForSeconds(5f);
        CanContinue.text = "Press any key to continue";
        btnContinue.interactable = true;
        showWarnning = true;
    }
}
