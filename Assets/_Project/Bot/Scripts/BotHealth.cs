using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHealth : MonoBehaviour, IDamegeble
{
    [SerializeField] private int maxHealth;
    
    public bool isDead = false;
    public Animator animator;

    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        Destroy(gameObject, 5);
    }
}
