using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePipeline
{
    public static HitResult HitDamage(DamageInfo damageInfo, Health health)
    {
        HitResult result = new HitResult();
        result.damageValue = damageInfo.damageValue;
        bool isCrit = Random.Range(0, 100) < damageInfo.critRate;
        if (isCrit)
        {
            result.isCrit = true;
            result.damageValue *= damageInfo.critMultiplier;
        }

        result.damageValue *= (1 - health.ArmorDamageReduction);
        result.damageValue = (float)System.Math.Round(result.damageValue, System.MidpointRounding.AwayFromZero);
        return result;
    } 
}

public class HitResult
{
    public float damageValue;
    public bool isCrit = false;

    public void Initialize(DamageInfo damageInfo)
    {
        damageValue = damageInfo.damageValue;
        isCrit = false;
    }
}
