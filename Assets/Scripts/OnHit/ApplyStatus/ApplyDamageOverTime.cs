using UnityEngine;
[System.Serializable]
public class ApplyDamageOverTime : ApplyStatusEffect
{
    [SerializeField] private DamageOverTimeSO damageOverTimeSO;
    public override StatusSO data => damageOverTimeSO;
    
    private DamageOverTimeSnapshot damageOverTimeSnapshot;

    public override StatusSnapshot dataSnapshot
    {
        get
        {
            if (damageOverTimeSnapshot == null)
            {
                damageOverTimeSnapshot = new DamageOverTimeSnapshot(damageOverTimeSO);
            }
            return damageOverTimeSnapshot;
        }
    }

    protected override void ApplyStatus(OnHitContext ctx)
    {
        if (damageOverTimeSnapshot == null)
        {
            damageOverTimeSnapshot = new DamageOverTimeSnapshot(damageOverTimeSO);
        }
        DamageOverTime dot = new DamageOverTime(damageOverTimeSnapshot, ctx.health);
        ctx.statusHandler.ReceiveStatus(dot);
    }

    public override OnHitEffect Clone()
    {
        var copy = (ApplyDamageOverTime)this.MemberwiseClone();
        copy.damageOverTimeSnapshot = null;
        return copy;
    }
    
}
