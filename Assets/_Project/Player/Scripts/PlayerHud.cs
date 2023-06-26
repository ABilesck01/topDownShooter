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
    [Header("Screen")]
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private GameObject ClassSelectionScreen;
    [Header("Health bar")]
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private Image hitImage;

    private Color hitColor;

    public PlayerHealth Player { get => player; set => player = value; }

    private void Start()
    {
        DeathScreen.SetActive(false);
        ClassSelectionScreen.SetActive(true);
    }

    public void Initialize(PlayerHealth player)
    {
        this.player = player;

        hitColor = hitImage.color;
        hitColor.a = 0;
        hitImage.color = hitColor;
        healthBar.Initialize(player.MaxHealth, true);
        player.OnDamageTaken.AddListener(OnDamageTaken);
        player.OnDeath.AddListener(OnPlayerDeath);
        player.OnRespawn.AddListener(OnPlayerRespawn);
    }

    private void OnDamageTaken(int value)
    {
        healthBar.SetProgress(value);
        hitColor.a = 0.8f;
        hitImage.color = hitColor;
        hitImage.DOFade(0, 0.25f);
    }

    private void OnPlayerDeath()
    {
        DeathScreen.SetActive(true);
    }

    private void OnPlayerRespawn()
    {
        DeathScreen.SetActive(false);
        ClassSelectionScreen.SetActive(true);
    }
}
