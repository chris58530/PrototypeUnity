using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace _.Scripts.Enemy.TypeA
{
    public class TypeAAction:Action
    {
        protected TypeAController controller;
        protected Transform player;
        protected NavMeshAgent navMeshAgent;
        protected Animator animator;
        protected Rigidbody rb;
    
        public override void OnAwake()
        {
            rb = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            EnemyAnimator enemyAnimator = GetComponent<EnemyAnimator>();
            animator = enemyAnimator.ani;
            controller = GetComponent<TypeAController>();
            player = UnityEngine.GameObject.FindWithTag("Player").transform;
        }
    }
}