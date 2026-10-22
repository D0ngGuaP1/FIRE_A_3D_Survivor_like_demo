using System;
using UnityEngine;
[CreateAssetMenu(fileName = "DoT", menuName = "Status/DoT")]
public class DamageOverTimeSO : StatusSO
{
    public float damage;
}

public class DamageOverTime : StatusInstance
{
    private float damage;
    private Health target;
    
    public DamageOverTime(DamageOverTimeSnapshot data, Health target)
    {
        duration = data.duration;
        interval = data.interval;
        hasInterval = data.hasInterval;
        damage = data.damage;
        timer = 0;
        canStack = data.canStack;
        stackLimit = data.stackLimit;
        stackCount = data.stackCount;
        this.target = target;
    }

    public override void Apply()
    {
        return;
    }
    protected override void OnUpdate()
    {
        target.TakeFloatDamage(damage * stackCount);
    }
    public override void Expire()
    {
        return;
    }

}

public class DamageOverTimeSnapshot : StatusSnapshot
{
    public float damage;

    public DamageOverTimeSnapshot(DamageOverTimeSO data) : base(data)
    {
        damage = data.damage;
    }
}

