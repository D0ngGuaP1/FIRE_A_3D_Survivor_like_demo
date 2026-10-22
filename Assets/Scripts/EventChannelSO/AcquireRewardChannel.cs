using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/AcquireRewardChannel")]
public class AcquireRewardChannel : ScriptableObject
{
    public event Action<RewardInfoSO> OnRaised;

    public void Raise(RewardInfoSO reward)
    {
        OnRaised?.Invoke(reward);
    }
}
