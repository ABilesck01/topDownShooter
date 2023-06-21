using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<PlayerInput> alliesTeam = new List<PlayerInput>();
    [SerializeField] private List<PlayerInput> axisTeam = new List<PlayerInput>();

    public void OnPlayerEnter(PlayerInput player)
    {
        AssignTeamToPlayer(player);
    }

    private void AssignTeamToPlayer(PlayerInput player)
    {
        if(alliesTeam.Count == 0)
        {
            alliesTeam.Add(player);
        }
        else if(axisTeam.Count == 0)
        {
            axisTeam.Add(player);
        }
        else if(alliesTeam.Count <= axisTeam.Count) 
        {
            alliesTeam.Add(player);
        }
        else
        {
            axisTeam.Add(player);
        }
    }
}
