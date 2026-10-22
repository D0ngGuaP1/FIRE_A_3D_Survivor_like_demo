using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponInfoSO", menuName = "Fire/WeaponInfo")]
public class WeaponInfoSO : ScriptableObject
{
    public float fireInterval;
    public int burstCount;
    public float burstInterval;
    [SerializeReference, SubclassSelector]
    public List<OnHitEffect> onHitEffects;

    public int hitCount;
}

public struct WeaponInfo
{
    public float fireInterval;
    public int burstCount;
    public float burstInterval;
    public  List<OnHitEffect> onHitEffects;
    public int hitCount;

    public WeaponInfo(WeaponInfoSO weaponInfoSO)
    {
        this.fireInterval = weaponInfoSO.fireInterval;
        this.burstCount = weaponInfoSO.burstCount;
        this.burstInterval = weaponInfoSO.burstInterval;
        this.onHitEffects = new List<OnHitEffect>(weaponInfoSO.onHitEffects.Count);
        foreach (var effect in weaponInfoSO.onHitEffects)
        {
            this.onHitEffects.Add(effect.Clone());
        }
        this.hitCount = weaponInfoSO.hitCount;
    }
}