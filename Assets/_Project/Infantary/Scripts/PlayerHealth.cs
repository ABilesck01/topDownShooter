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

    public UnityAction<int> OnDamageTaken;
    public UnityAction OnDeath;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }
}
