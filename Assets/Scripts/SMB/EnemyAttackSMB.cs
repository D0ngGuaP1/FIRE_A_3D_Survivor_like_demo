using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSMB : StateMachineBehaviour
{
    [SerializeField] private string triggerName = "Attack";
    //[SerializeField] private string boolName = "IsAttacking";
    private int triggerHash;
    //private int boolHash;
    private Enemy e;
    
    private bool hasAttacked = false;

    [SerializeField] private float dealDamageThreshold = 0.3f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasAttacked = false;
        triggerHash = Animator.StringToHash(triggerName);
        //boolHash = Animator.StringToHash(boolName);
        if (e == null) e = animator.GetComponent<Enemy>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hasAttacked) return;
        if (stateInfo.normalizedTime >= dealDamageThreshold)
        {
            hasAttacked = true;
            if(e) e.DealDamage();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (string.IsNullOrEmpty(triggerName))
        {
            Debug.LogWarning("No state name provided");
            return;
        }
        animator.ResetTrigger(triggerHash);
        //animator.SetBool(boolHash, false);
        e.isAttacking = false;
    }
}
