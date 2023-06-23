using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/New class")]
public class PlayerClass : ScriptableObject
{
    public string className;
    public int cost;
    public PlayerClassController player;
}
