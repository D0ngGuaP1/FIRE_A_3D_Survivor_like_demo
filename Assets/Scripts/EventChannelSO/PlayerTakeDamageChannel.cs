using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/PlayerTakeDamageChannel")]
public class PlayerTakeDamageChannel : ScriptableObject
{
    public Action<float, float> OnRaised;

    public void Raise(float currentHealth, float maxHealth)
    {
        OnRaised?.Invoke(currentHealth, maxHealth);
    }
}
