using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    [SerializeField] private Transform[] alliesSpawns;
    [SerializeField] private Transform[] axisSpawns;


    public static SpawnPointController instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform GetRandomAlliesSpawn()
    {
        int rand = Random.Range(0, alliesSpawns.Length);
        return alliesSpawns[rand];
    }

    public Transform GetRandomAxisSpawn()
    {
        int rand = Random.Range(0, axisSpawns.Length);
        return axisSpawns[rand];
    }
}
