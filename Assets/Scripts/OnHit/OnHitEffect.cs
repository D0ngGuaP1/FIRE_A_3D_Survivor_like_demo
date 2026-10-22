[System.Serializable]
public abstract class OnHitEffect
{
    public void Apply(OnHitContext ctx)
    {
        OnApply(ctx);
    }
    protected  abstract void OnApply(OnHitContext ctx);
    
    public abstract OnHitEffect Clone();
}
