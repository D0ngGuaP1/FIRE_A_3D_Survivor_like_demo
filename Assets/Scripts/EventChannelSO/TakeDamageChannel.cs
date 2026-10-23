using System;
using UnityEngine;
[CreateAssetMenu(fileName = "EventChannel", menuName = "Events/TakeDamageChannel")]
public class TakeDamageChannel : ScriptableObject
{
    public event Action<OnHitContext> OnRaised;

    public void Raise(OnHitContext ctx)
    {
        OnRaised?.Invoke(ctx);
    }
}


