using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotController : TeamController
{
    [SerializeField] private EnemyClassController alliesEnemyController;
    [SerializeField] private EnemyClassController axisEnemyController;

    public UnityEvent OnRespawn;

    private void Start()
    {
        OnRespawn.AddListener(InstantiateEnemy);
        InstantiateEnemy();
    }

    private void InstantiateEnemy()
    {
        EnemyClassController bot;
        if (Team == PlayerTeam.Allies)
        {
            Transform spawnPoint = SpawnPointController.instance.GetRandomAlliesSpawn();
            bot = Instantiate(alliesEnemyController, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Transform spawnPoint = SpawnPointController.instance.GetRandomAxisSpawn();
            bot = Instantiate(axisEnemyController, spawnPoint.position, spawnPoint.rotation);
        }

        bot.BotHealth.OnDeath.AddListener(OnPlayerDeath);
        bot.BotHealth.OnRespawn = OnRespawn;
    }
}
