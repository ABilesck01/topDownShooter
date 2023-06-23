using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClassView : MonoBehaviour
{
    [SerializeField] private PlayerClass[] classes;
    [Space]
    [SerializeField] private GameObject screen;
    [SerializeField] private PlayerClassViewItem itemPrefab;
    [SerializeField] private Transform container;
    [Space]
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        int index = 0;
        for (int i = 0; i < classes.Length; i++)
        {
            index = i;
            var item = Instantiate(itemPrefab, container);
            item.Fill(classes[index].className, () =>
            {
                Instantiate(classes[index].player, spawnPoint.position, spawnPoint.rotation);
                screen.SetActive(false);
            });
        }
    }
}