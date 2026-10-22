using System;
using UnityEngine;

public class Health : MonoBehaviour, IContextDamageable, IFloatDamageable, IExecuteable
{
    [SerializeField]private EnemyInfoSO enemyInfoSO;
    private EnemyInfo enemyInfo;
    public float maxHealth { get; private set; } = 100f;
    public float Current { get; private set; }
    public bool IsDead => Current <= 0;

    public event Action OnDied;  // 死亡事件，供动画/销毁/UI订阅
    
    [SerializeField] private TakeDamageChannel takeDamageChannel;
    
    private OnHitContext _ctx;

    public float ArmorDamageReduction
    {
        get
        {
            return enemyInfo.armor / (100f + enemyInfo.armor);
        }
    }

    public void Awake()
    {
        enemyInfo = new EnemyInfo(enemyInfoSO);
        _ctx = new OnHitContext();
    }
    
    public void TakeDamage(OnHitContext ctx)
    {
        if (IsDead) return;
        Current -= ctx.damageValue;
        
        Debug.Log($"[Kill] monster take damage, channel ID = {takeDamageChannel.GetInstanceID()}");
        
        takeDamageChannel.Raise(ctx);
        if (IsDead)
        {
            
            OnDied?.Invoke();
        }
    }
    public void TakeFloatDamage(float damage)
    {
        _ctx.Clear();
        if (IsDead) return;
        Current -= damage;
        _ctx.hitPosition = transform.position;
        _ctx.damageValue = damage;
        _ctx.isCrit = false;
        takeDamageChannel.Raise(_ctx);
        if (IsDead) OnDied?.Invoke();
    }

    public void Execute()
    {
        TakeFloatDamage(Current);
    }

    private void OnEnable()
    {
        maxHealth = enemyInfo.maxHealth;
        Current = maxHealth;
    }
}
