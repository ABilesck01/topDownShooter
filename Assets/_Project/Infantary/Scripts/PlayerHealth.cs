using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamegeble
{
    [SerializeField] private int maxHealth;
    
    public bool isDead = false;
    
    private int currentHealth;

    public UnityEvent<int> OnDamageTaken;
    public UnityEvent OnDeath;
    public UnityEvent OnRespawn;
    public Animator animator;

    public int MaxHealth { get => maxHealth; set => MaxHealth = value; }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Respawn()
    {
        OnRespawn?.Invoke();
        Destroy(gameObject);
    }

    public bool TakeDamage(int amount)
    {
        if(isDead) return false;

        currentHealth -= amount;
        OnDamageTaken?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    [ContextMenu("Test Damage")]
    public void TakeDamage()
    {
        TakeDamage(10);
    }

    [ContextMenu("Kill player")]
    public void KillPlayer()
    {
        TakeDamage(maxHealth);
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        OnDeath?.Invoke();
        StartCoroutine(WaitForRespawn());
    }

    private IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(5);
        Respawn();
    }
}
