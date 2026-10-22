using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseCritRateReward", menuName = "Reward/IncreaseCritRateReward")]
public class IncreaseCritRateRewardSO : RewardInfoSO
{
    [SerializeField] private float value;
    public override void OnAcquire(MainCharacter character)
    {
        var weapon = character.GetComponentInChildren<Weapon>();
        if (weapon)
        {
            weapon.damageInfo.critRate += value;
        }
    }
}
