using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/EnemyDeathChannel")]
public class EnemyDeathChannel : ScriptableObject
{
    public event Action<EnemyDeathInfo> OnRaised;
    
    public void Raise(EnemyDeathInfo info){
        OnRaised?.Invoke(info);
    }
}

public readonly struct EnemyDeathInfo
{
    public readonly Vector3 position;
    public readonly EnemyInfo info;

    public EnemyDeathInfo(Vector3 position, EnemyInfo info)
    {
        this.position = position;
        this.info = info;
    }
}