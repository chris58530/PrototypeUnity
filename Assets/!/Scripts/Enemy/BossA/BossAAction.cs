using _.Scripts.Enemy.BossA;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace _.Scripts.Enemy.TypeA
{
    public class BossAAction:Action
    {
        protected BossAController controller;
        protected BossABase bossBase;
        protected Transform player;
        protected NavMeshAgent navMeshAgent;
        protected Animator animator;
        protected Rigidbody rb;
    
        public override void OnAwake()
        {
            bossBase = GetComponent<BossABase>();
            rb = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            EnemyAnimator enemyAnimator = GetComponent<EnemyAnimator>();
            animator = enemyAnimator.ani;
            controller = GetComponent<BossAController>();
            player = UnityEngine.GameObject.FindWithTag("Player").transform;
        }
    }
}