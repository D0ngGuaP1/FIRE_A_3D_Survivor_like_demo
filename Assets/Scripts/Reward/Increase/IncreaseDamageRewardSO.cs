using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseDamageReward", menuName = "Reward/IncreaseDamageReward")]
public class IncreaseDamageRewardSO : RewardInfoSO
{
    [SerializeField] private float multiplier;
    public override void OnAcquire(MainCharacter character)
    {
        var weapon = character.GetComponentInChildren<Weapon>();
        if (weapon)
        {
            float amount = weapon.originalDamage * (multiplier);
            amount = (float)System.Math.Round(amount, System.MidpointRounding.AwayFromZero);
            weapon.damageInfo.damageValue += amount;
        }
    }
}
