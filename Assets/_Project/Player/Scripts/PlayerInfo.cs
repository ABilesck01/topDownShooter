using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private PlayerTeam team;

    public PlayerTeam Team { get => team; set => team = value; }
}
public enum PlayerTeam
{
    Allies,
    Axis,
    Auto
}