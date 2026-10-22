using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/PlayerDeathChannel")]
public class PlayerDeathChannel : ScriptableObject
{
    public event Action OnRaised;

    public void Raise()
    {
        OnRaised?.Invoke();
    }
}
