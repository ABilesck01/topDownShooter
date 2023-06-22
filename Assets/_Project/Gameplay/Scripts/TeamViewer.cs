using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeamViewer : MonoBehaviour
{
    [SerializeField] private Transform alliesContainer;
    [SerializeField] private Transform axisContainer;
    [Space] 
    [SerializeField] private TeamViewerItem itemTemplate;

    public void FillAllies(List<PlayerInput> playerList)
    {
        foreach (Transform child in alliesContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (PlayerInput playerInput in playerList)
        {
            var item = Instantiate(itemTemplate, alliesContainer);
            item.Fill($"Player {playerInput.playerIndex}");
        }
    }
    
    public void FillAxis(List<PlayerInput> playerList)
    {
        foreach (Transform child in axisContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (PlayerInput playerInput in playerList)
        {
            var item = Instantiate(itemTemplate, axisContainer);
            item.Fill($"Player {playerInput.splitScreenIndex + 1}");
        }
    }
}
