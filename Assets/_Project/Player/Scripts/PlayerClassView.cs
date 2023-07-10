using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerClassView : TeamController
{
    [SerializeField] private PlayerClass[] classes;
    [Space]
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtCost;
    [SerializeField] private TextMeshProUGUI txtHealth;
    [SerializeField] private TextMeshProUGUI txtDmg;
    [SerializeField] private TextMeshProUGUI txtDescr;
    [SerializeField] private TextMeshProUGUI txtPoints;
    [SerializeField] private Image classImage;
    [Space]
    [SerializeField] private GameObject screen;
    [SerializeField] private PlayerClassViewItem itemPrefab;
    [SerializeField] private Transform container;
    [Space]
    [SerializeField] private MultiplayerEventSystem myEventSystem;
    [SerializeField] private PlayerHud hud;
    [SerializeField] private WeaponView weaponView;
    [SerializeField] private InfantaryInputs infantaryInputs;
    [SerializeField] private CameraFollow cameraFollow;

    private int points = 0;

    public int Points
    {
        get => points; 
        set
        {
            points = value;
            txtPoints.text = points.ToString("000");
        }
    }

    private void Start()
    {
        List<Button> allButtons = new List<Button>();

        for (int i = 0; i < classes.Length; i++)
        {
            int index = i;
            var item = Instantiate(itemPrefab, container);
            item.Fill(classes[index].className, () =>
            {
                InstantiatePlayer(index);
            },
            () =>
            {
                txtName.text = classes[index].name;
                txtCost.text = classes[index].cost.ToString();
                txtHealth.text = classes[index].MaxHealth.ToString();
                txtDmg.text = classes[index].damage.ToString();
                txtDescr.text = classes[index].Description;
                if(Team == PlayerTeam.Allies)
                    classImage.sprite = classes[index].alliesImage;
                else
                    classImage.sprite = classes[index].axisImage;
            });

            allButtons.Add(item.BtnClass);

            if(i==0)
                myEventSystem.SetSelectedGameObject(item.gameObject);
        }

        ArrangeButtons(allButtons);
    }

    private void ArrangeButtons(List<Button> buttons)
    {
        if (buttons != null && buttons.Count > 1)
        {
            for (int i = 0; i < buttons.Count; i++)
            {

                Navigation navigation = buttons[i].navigation;
                navigation.mode = Navigation.Mode.Explicit;

                if (i > 0 && i < buttons.Count - 1)
                {
                    navigation.selectOnLeft = buttons[i - 1];
                    navigation.selectOnRight = buttons[i + 1];
                }
                else if (i == 0)
                {
                    navigation.selectOnLeft = null;
                    navigation.selectOnRight = buttons[i + 1];
                }
                else if (i == buttons.Count - 1)
                {
                    navigation.selectOnLeft = buttons[i - 1];
                    navigation.selectOnRight = null;
                }

                buttons[i].navigation = navigation;
            }
        }
    }

    private void InstantiatePlayer(int index)
    {
        if(points < classes[index].cost)
        {
            Debug.Log("Dont have enough points");
            return;
        }

        Debug.Log($"Selected class {classes[index].className}");

        PlayerClassController player;
        if (Team == PlayerTeam.Allies)
        {
            Transform spawnPoint = SpawnPointController.instance.GetRandomAlliesSpawn();
            player = Instantiate(classes[index].playerAllies, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Transform spawnPoint = SpawnPointController.instance.GetRandomAxisSpawn();
            player = Instantiate(classes[index].playerAxis, spawnPoint.position, spawnPoint.rotation);
        }

        player.InfantaryInputs = infantaryInputs;
        player.Controller.SetInfantaryInput(infantaryInputs);
        player.Weapon.SetInfantaryInput(infantaryInputs);
        player.PlayerClass = classes[index];
        player.Weapon.WeaponView = weaponView;

        player.PlayerHealth.OnDeath.AddListener(OnPlayerDeath);

        player.Weapon.OnKillPlayer.AddListener(AddPoints);

        hud.Initialize(player.PlayerHealth);
        cameraFollow.Target = player.transform;
        screen.SetActive(false);
    }

    private void AddPoints()
    {
        Points += 100;
    }
}