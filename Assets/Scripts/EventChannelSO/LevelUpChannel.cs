using System;
using UnityEngine;
[CreateAssetMenu(fileName = "EventChannel", menuName = "Events/LevelUpChannel")]
public class LevelUpChannel : ScriptableObject
{
    public event Action<int> OnRaised;

    public void Raise(int level)
    {
        OnRaised?.Invoke(level);
    }
}
