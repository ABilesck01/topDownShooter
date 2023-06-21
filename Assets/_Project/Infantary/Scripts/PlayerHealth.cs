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
    public Animator animator;

    public int MaxHealth { get => maxHealth;}

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        OnDamageTaken?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [ContextMenu("Test Damage")]
    public void TakeDamage()
    {
        currentHealth -= 10;
        OnDamageTaken?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        OnDeath?.Invoke();
    }
}
