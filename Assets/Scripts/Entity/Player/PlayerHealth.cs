using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IFloatDamageable
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    
    [SerializeField] private PlayerDeathChannel playerDeathChannel;
    [SerializeField] private PlayerTakeDamageChannel takeDamageChannel;
    
    private bool isDead = false;

    void OnEnable()
    {
        isDead = false;
        currentHealth = maxHealth;
    }
    
    public void TakeFloatDamage(float damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        takeDamageChannel.Raise(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            playerDeathChannel.Raise();
        }
    }
}