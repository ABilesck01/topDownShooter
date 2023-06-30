using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotHealth : MonoBehaviour, IDamegeble
{
    [SerializeField] private int maxHealth;
    
    public bool isDead = false;
    public Animator animator;


    private int currentHealth;

    public UnityEvent OnDeath;
    public UnityEvent OnRespawn;

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

    private IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(5);
        Respawn();
    }

    public void Respawn()
    {
        OnRespawn?.Invoke();
        Destroy(gameObject);
    }

    private void Die()
    {
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Default");
        gameObject.layer = LayerIgnoreRaycast;
        isDead = true;
        animator.SetTrigger("isDead");
        OnDeath?.Invoke();
        StartCoroutine(WaitForRespawn());
    }
}
