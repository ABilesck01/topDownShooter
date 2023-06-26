using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<PlayerClassView> alliesTeam = new List<PlayerClassView>();
    [SerializeField] private List<PlayerClassView> axisTeam = new List<PlayerClassView>();

    public void OnPlayerEnter(PlayerInput player)
    {
        AssignTeamToPlayer(player.GetComponent<PlayerClassView>());
    }

    private void AssignTeamToPlayer(PlayerClassView player)
    {
        if(alliesTeam.Count == 0)
        {
            alliesTeam.Add(player);
            player.Team = PlayerTeam.Allies;
            return;
        }

        if(axisTeam.Count == 0)
        {
            axisTeam.Add(player);
            player.Team = PlayerTeam.Axis;
            return;
        }

        if(alliesTeam.Count <= axisTeam.Count)
        {
            alliesTeam.Add(player);
            player.Team = PlayerTeam.Allies;
        }
        else
        {
            axisTeam.Add(player);
            player.Team = PlayerTeam.Axis;
        }
    }
}
