using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float timeToRespawn;
    private Vector3 spawnPosition;
    private PlayerHealth health;

    private void Start()
    {
        spawnPosition = transform.localPosition;
        health = GetComponent<PlayerHealth>();
        health.OnDeath.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(timeToRespawn);

        transform.localPosition = spawnPosition;
        health.Respawn();
    }
}
