using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private DamageInfo damageInfo;

    [SerializeField] private float speed = 30f;
    [SerializeField] private float timeToLive = 3f;
    private float currentTime;
    private Rigidbody rb;

    private System.Action releaseAction;
    
    private List<OnHitEffect> onHitEffects;
    
    private OnHitContext onHitContext;
    
    private HashSet<Health> hitHealth;
    private int hitCount = 2;
    
    private HitResult hitResult;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        onHitEffects = new List<OnHitEffect>();
        onHitContext = new OnHitContext();
        hitHealth =  new HashSet<Health>();
        hitResult = new HitResult();
    }
    public void Launch(List<OnHitEffect> effects, Vector3 position, Quaternion rotation, DamageInfo info, System.Action OnDespawn, int hitCount)
    {
        currentTime = 0f;
        damageInfo = info;
        releaseAction = OnDespawn;
        transform.SetPositionAndRotation(position, rotation);
        rb.velocity = transform.forward * speed;
        onHitEffects = effects;
        onHitContext.Clear();
        hitHealth.Clear();
        this.hitCount = hitCount;
        hitResult.Initialize(damageInfo);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToLive)
        {
            releaseAction?.Invoke();
            return;
        }
        rb.velocity = transform.forward * speed;
    }
    void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if (!health) return;
        
        if (hitHealth.Contains(health)) return;
        hitHealth.Add(health);

        hitResult = DamagePipeline.HitDamage(damageInfo, health);
        onHitContext.damageValue = hitResult.damageValue;
        onHitContext.isCrit =  hitResult.isCrit;
        onHitContext.target =  other.gameObject;
        onHitContext.health = health;
        onHitContext.statusHandler = other.GetComponent<StatusHandler>();
        onHitContext.hitPosition = other.transform.position;
        onHitContext.hitDirection = transform.forward.normalized;

        foreach (var effect in onHitEffects)
        {
            effect.Apply(onHitContext);
        }

        hitCount--;
        if(hitCount <= 0) releaseAction?.Invoke();
        damageInfo.damageValue *= damageInfo.throughMultiplier;
    }

}



