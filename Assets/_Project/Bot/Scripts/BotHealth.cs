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

    public bool TakeDamage(int amount)
    {
        if (isDead) return false;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    [ContextMenu("Kill")]
    public void Kill()
    {
        TakeDamage(maxHealth);
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("isDead");
        Destroy(gameObject, 5);
    }
}
