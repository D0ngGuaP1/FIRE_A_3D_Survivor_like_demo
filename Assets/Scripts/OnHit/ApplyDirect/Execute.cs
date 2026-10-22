using UnityEngine;
[System.Serializable]
public class Execute : OnHitEffect
{
    [SerializeField] private float threshold = 0.2f;
    protected override void OnApply(OnHitContext ctx)
    {
        if (ctx.target == null) return;

        var health = ctx.target.GetComponentInParent<Health>();
        if (health == null || health.IsDead) return;

        if (health.Current / health.maxHealth > threshold) return;

        health.Execute();
        Debug.Log("Executed");
    }

    public override OnHitEffect Clone()
    {
        return this;
    }
}
