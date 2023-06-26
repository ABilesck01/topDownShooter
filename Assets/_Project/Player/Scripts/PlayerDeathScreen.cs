using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private GameObject ClassSelectionScreen;

    public void Initialize()
    {

    }

    private void OnPlayerDeath()
    {
        DeathScreen.SetActive(true);
    }
}
