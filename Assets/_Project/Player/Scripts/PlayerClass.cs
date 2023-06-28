using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/New class")]
public class PlayerClass : ScriptableObject
{
    [Header("Common settings")]
    public string className;
    public int cost;
    public int MaxHealth;
    [Header("Weapon")]
    public int damage;
    public int fireRate;
    public int ammo;
    [TextArea(3,10)]
    public string Description;
    [Space]
    [Header("Allies")]
    public Sprite alliesImage;
    public PlayerClassController playerAllies;
    [Header("Axis")]
    public Sprite axisImage;
    public PlayerClassController playerAxis;
}
