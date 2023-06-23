using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    [Space]
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private Image hitImage;

    private Color hitColor;

    private void Start()
    {
        hitColor = hitImage.color;
        hitColor.a = 0;
        hitImage.color = hitColor;
        healthBar.Initialize(player.MaxHealth, true);
        player.OnDamageTaken.AddListener(OnDamageTaken);
        player.OnRespawn.AddListener(OnRespawn);
    }

    private void OnRespawn(int value)
    {
        healthBar.SetProgress(value);
    }

    private void OnDamageTaken(int value)
    {
        healthBar.SetProgress(value);
        hitColor.a = 0.8f;
        hitImage.color = hitColor;
        hitImage.DOFade(0, 0.25f);
    }
}
