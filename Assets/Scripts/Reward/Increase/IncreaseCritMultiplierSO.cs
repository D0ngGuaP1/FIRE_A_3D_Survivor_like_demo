using UnityEngine;
[CreateAssetMenu(fileName = "IncreaseCritMultiplier", menuName = "Reward/IncreaseCritMultiplier")]
public class IncreaseCritMultiplierSO : RewardInfoSO
{
    [SerializeField] private float increase;
    public override void OnAcquire(MainCharacter character)
    {
        var weapon = character.GetComponentInChildren<Weapon>();
        if (weapon)
        {
            weapon.damageInfo.critMultiplier += increase;
        }
    }
}
