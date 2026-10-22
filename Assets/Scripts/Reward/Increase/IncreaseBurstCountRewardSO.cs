using UnityEngine;
[CreateAssetMenu(fileName = "IncreaseBurstCountReward", menuName = "Reward/IncreaseBurstCountReward")]
public class IncreaseBurstCountRewardSO : RewardInfoSO
{
    [SerializeField] private int amount;
    public override void OnAcquire(MainCharacter character)
    {
        var weapon = character.GetComponentInChildren<Weapon>();
        if (weapon)
        {
            weapon.weaponInfo.burstCount += amount;
        }
    }
}
