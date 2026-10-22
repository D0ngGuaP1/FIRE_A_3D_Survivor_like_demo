using UnityEngine;
[System.Serializable]
public abstract class ApplyStatusEffect : OnHitEffect
{
    public abstract StatusSO data { get; }
    public abstract StatusSnapshot dataSnapshot { get; }
    protected override void OnApply(OnHitContext ctx)
    {
        var handler = ctx.target.GetComponent<StatusHandler>();
        if (!handler) return;
        ApplyStatus(ctx);
    }
    protected abstract void ApplyStatus(OnHitContext ctx);
    public abstract override  OnHitEffect Clone();
}
