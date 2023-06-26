using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassController : MonoBehaviour
{
    [SerializeField] private PlayerClass playerClass;
    [SerializeField] private InfantaryInputs infantaryInputs;
    [Space]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InfantryGamepadController controller;
    [SerializeField] private WeaponController weapon;
    [SerializeField] private PlayerTeamController teamController;

    public PlayerClass PlayerClass { get => playerClass; set => playerClass = value; }
    public PlayerHealth PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public InfantaryInputs InfantaryInputs { get => infantaryInputs; set => infantaryInputs = value; }
    public InfantryGamepadController Controller { get => controller; set => controller = value; }
    public WeaponController Weapon { get => weapon; set => weapon = value; }
    public PlayerTeamController TeamController { get => teamController; set => teamController = value; }
}
