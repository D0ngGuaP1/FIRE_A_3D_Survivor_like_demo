using UnityEngine;
[System.Serializable]
public class DirectDamage : OnHitEffect
{
    protected override void OnApply(OnHitContext ctx)
    {
        if (ctx.health == null)
        {
            Debug.LogError("No Health component attached to this object: " + ctx.target.name);
            return;
        }
        ctx.health.TakeDamage(ctx);
    }

    public override OnHitEffect Clone()
    {
        return this;
    }
}
