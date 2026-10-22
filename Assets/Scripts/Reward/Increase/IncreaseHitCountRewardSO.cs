using UnityEngine;
[CreateAssetMenu(fileName = "IncreaseHitCountReward", menuName = "Reward/IncreaseHitCountReward")]
public class IncreaseHitCountRewardSO : RewardInfoSO
{
    [SerializeField] private int amount;
    public override void OnAcquire(MainCharacter character)
    {
        var weapon = character.GetComponentInChildren<Weapon>();
        if (weapon)
        {
            weapon.weaponInfo.hitCount += amount;
        }
    }
}
