using UnityEngine;

[System.Serializable]
public class KnockBack : OnHitEffect
{
    protected override void OnApply(OnHitContext ctx)
    {
        Rigidbody rb = ctx.target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(ctx.hitDirection * 25f, ForceMode.VelocityChange);
        }
        else ctx.target.transform.Translate(ctx.hitDirection * 0.5f, Space.World);
    }

    public override OnHitEffect Clone()
    {
        return this;
    }
}
