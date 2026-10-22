using UnityEngine;
public abstract class RewardInfoSO : ScriptableObject
{
    public string displayName;
    public string description;

    public abstract void OnAcquire(MainCharacter character);
}
