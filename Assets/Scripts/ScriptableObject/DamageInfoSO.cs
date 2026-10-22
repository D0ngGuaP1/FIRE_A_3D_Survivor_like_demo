using UnityEngine;

[CreateAssetMenu(fileName = "DamageInfoSO", menuName = "Fire/DamageInfo")]
public class DamageInfoSO : ScriptableObject
{
    public float damageValue;
    public float critRate;
    public float critMultiplier;
    public float throughMultiplier;
}

public struct DamageInfo
{
    public float damageValue;
    public float critRate;
    public float critMultiplier;
    public float throughMultiplier;

    public DamageInfo(DamageInfoSO damageInfoSo)
    {
        this.damageValue = damageInfoSo.damageValue;
        this.critRate = damageInfoSo.critRate;
        this.critMultiplier = damageInfoSo.critMultiplier;
        this.throughMultiplier = damageInfoSo.throughMultiplier;
    }
}