using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using UnityEngine;


public class EnemyAction : Action
{
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;

    public override void OnAwake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        EnemyAnimator enemyAnimator = GetComponent<EnemyAnimator>();
        animator = enemyAnimator.ani;
    }
}