using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private Health health;
    private Rigidbody rb;
    private Animator animator;
    
    public bool isAttacking = false;
    [SerializeField] private float attackCooldown = 1.2f;
    private float nextAttackTime;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private System.Action releaseAction;
    
    [SerializeField] private EnemyDeathChannel deathChannel;
    
    [SerializeField] private EnemyInfoSO enemyInfoSO;
    private EnemyInfo enemyInfo;
    [SerializeField] private float attackRange = 1.5f;

    private MainCharacter target;
    private float sqrDistance = 0f;
    
    private Vector3 targetOffset;
    
    [Header("分离设置")]
    [SerializeField] private float separationRadius = 1.5f;    // 感知邻居的范围
    [SerializeField] private float separationStrength = 8f;    // 排斥力度
    [SerializeField] private LayerMask enemyLayer;             // 敌人所在层
    [SerializeField] private float targetOffsetRadius = 1f;    // 围绕玩家的目标偏移半径
    
    private static readonly Collider[] s_Neighbors = new Collider[16]; 
    

    private void Awake()
    {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody>();
        enemyInfo = new EnemyInfo(enemyInfoSO);
        animator = GetComponent<Animator>();
        
    }
    
    public void Spawn(System.Action OnDespawn, MainCharacter target)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        health.OnDied -= OnDeath;
        health.OnDied += OnDeath;
        
        releaseAction = OnDespawn;
        
        isAttacking = false;
        nextAttackTime = Time.time;
        
        animator.Rebind();
        animator.ResetTrigger(Attack);
        animator.SetBool(IsMoving, false);
        animator.Update(0f);
        
        this.target = target;
        
        // 基于敌人当前位置相对玩家的方位，分配一个环绕偏移
        Vector3 dir = transform.position - target.transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.01f)                     // 完全重合时随机一个方向
        {
            Vector2 r = Random.insideUnitCircle.normalized;
            dir = new Vector3(r.x, 0f, r.y);
        }
        targetOffset = dir.normalized * targetOffsetRadius;
    }

    private void Update()
    {
        if(!target) return;
        
        animator.SetBool(IsMoving, rb.velocity.sqrMagnitude > 0.01f);
        
        if(isAttacking) return;
        if(nextAttackTime > Time.time) return;
        
        sqrDistance = (transform.position - target.transform.position).sqrMagnitude;
        
        if (sqrDistance < attackRange * attackRange)
        {
            isAttacking = true;
            nextAttackTime = Time.time + attackCooldown;
            animator.SetTrigger(Attack);
        }
    }

    private void OnDeath()
    {
        health.OnDied -= OnDeath;
        
        deathChannel.Raise(new EnemyDeathInfo(transform.position, enemyInfo));
        
        releaseAction?.Invoke();
        
    }
    
    private void OnDisable()
    {
        health.OnDied -= OnDeath;
        releaseAction = null;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        // 朝向"目标点 + 随机偏移"
        Vector3 toTarget = (target.transform.position + targetOffset) - transform.position;
        toTarget.y = 0f;
        if (toTarget.sqrMagnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(toTarget.normalized);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, lookRotation, 720f * Time.fixedDeltaTime);
        }

        // 移动速度 + 分离力
        Vector3 velocity = isAttacking
            ? Vector3.zero                                                  // 攻击中：站定
            : transform.forward * enemyInfo.velocity + ComputeSeparation(); // 否则：追踪
        velocity.y = rb.velocity.y;   // 始终保留重力
        
        rb.velocity = velocity;
        
    }

    private Vector3 ComputeSeparation()
    {
        Vector3 separation = Vector3.zero;
        int count = Physics.OverlapSphereNonAlloc(
            transform.position, separationRadius, s_Neighbors, enemyLayer);

        for (int i = 0; i < count; i++)
        {
            if (s_Neighbors[i].attachedRigidbody == rb) continue; // 跳过自己

            Vector3 diff = transform.position - s_Neighbors[i].transform.position;
            diff.y = 0f;
            float dist = diff.magnitude;

            if (dist < 0.001f) // 完全重合时给随机方向弹开
            {
                diff = Random.insideUnitSphere; diff.y = 0f;
                dist = 1f;
            }

            // 越近推力越大（线性衰减）
            separation += diff.normalized * (1f - dist / separationRadius);
        }

        return separation * separationStrength;
    }

    public void DealDamage()
    {
        if(!target) return;
        
        sqrDistance = (transform.position - target.transform.position).sqrMagnitude;
        if(sqrDistance < attackRange * attackRange)
        {
            target.TakeDamage(enemyInfo.attackDamage);
        }
    }

    public void ApplyPowerUp(float multiplier)
    {
        enemyInfo.attackDamage *= multiplier;
    }
}
