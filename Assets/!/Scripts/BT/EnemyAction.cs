using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAction : Action
{
    protected NavMeshAgent navMeshAgent;
    protected Rigidbody rb;

    public override void OnAwake()
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        EnemyAnimator enemyAnimator = GetComponent<EnemyAnimator>();
    }
}
