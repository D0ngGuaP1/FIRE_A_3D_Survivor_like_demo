using System;
using UnityEngine;
[CreateAssetMenu(fileName = "EventChannel", menuName = "Events/TakeDamageChannel")]
public class TakeDamageChannel : ScriptableObject
{
    public event Action<OnHitContext> OnRaised;

    public void Raise(OnHitContext ctx)
    {
        Debug.Log($"[{name}] raised, ID = {GetInstanceID()}, listeners = {(OnRaised != null ? OnRaised.GetInvocationList().Length : 0)}");
        OnRaised?.Invoke(ctx);
    }
}


