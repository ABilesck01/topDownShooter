using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClassController : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private BotHealth botHealth;
    [SerializeField] private BotAttack botAttack;

    public EnemyController EnemyController { get => enemyController; set => enemyController = value; }
    public BotHealth BotHealth { get => botHealth; set => botHealth = value; }
    public BotAttack BotAttack { get => botAttack; set => botAttack = value; }
}
