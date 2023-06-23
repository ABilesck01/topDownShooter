using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public BotState botState;

    private BotAttack botAttack;
    private BotMovement botMovement;
    private ConeView coneView;
    private BotHealth botHealth;

    private void Awake()
    {
        botAttack = GetComponent<BotAttack>();
        botMovement = GetComponent<BotMovement>();
        botHealth = GetComponent<BotHealth>();
        coneView = GetComponent<ConeView>();
    }

    private void Update()
    {
        if (botHealth.isDead)
        {
            botState = BotState.dead;
            return;
        }

        if(coneView.currentState == ConeViewState.noView)
        {
            botState = BotState.patrol;
            return;
        }

        if(coneView.currentState == ConeViewState.onView)
        {
            botState = BotState.follow;
            return;
        }

        if(coneView.currentState == ConeViewState.onRange)
        {
            botMovement.StopAgent();
            botState = BotState.attack;
            return;
        }

    }
}

public enum BotState
{
    patrol,
    follow,
    attack,
    dead
}
