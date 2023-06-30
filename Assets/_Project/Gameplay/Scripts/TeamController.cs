using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeamController : MonoBehaviour
{
    [Space]
    [SerializeField] private PlayerTeam team;
    public UnityAction OnPlayerDeath;

    public PlayerTeam Team { get => team; set => team = value; }
}
