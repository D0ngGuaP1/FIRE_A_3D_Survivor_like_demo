using UnityEngine;
[CreateAssetMenu(fileName = "DecreaseFireIntervalReward", menuName = "Reward/DecreaseFireInterval")]

public class DecreaseFireIntervalRewardSO : RewardInfoSO
{
    [SerializeField] private float value;
    public override void OnAcquire(MainCharacter character)
    {
        var weapon = character.GetComponentInChildren<Weapon>();
        if (weapon)
        {
            weapon.weaponInfo.fireInterval -= value;
            if (weapon.weaponInfo.fireInterval <= 0.3f)
            {
                weapon.weaponInfo.fireInterval = 0.3f;
            }
        }
    }
}
