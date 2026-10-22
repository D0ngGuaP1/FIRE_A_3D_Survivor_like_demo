using UnityEngine;
[CreateAssetMenu(fileName = "Apply", menuName = "Reward/ApplyOnHitEffect")]
public class AcquireOnHitEffectRewardSO : RewardInfoSO
{
    [SerializeField, SerializeReference, SubclassSelector] private OnHitEffect effect;

    public override void OnAcquire(MainCharacter character)
    {
        var weapon = character.GetComponentInChildren<Weapon>();
        foreach (var effect in weapon.weaponInfo.onHitEffects)
        {
            if (this.effect.GetType() == effect.GetType())
            {
                if (this.effect is ApplyStatusEffect ase1 && effect is ApplyStatusEffect ase2)
                {
                    if (ase1.dataSnapshot.canStack && ase2.dataSnapshot.canStack)
                    {
                        ase2.dataSnapshot.stackLimit++;
                        return;
                    }
                    else return;
                }
                else return;
            }
        }

        weapon.weaponInfo.onHitEffects.Add(this.effect.Clone());
    }
}
