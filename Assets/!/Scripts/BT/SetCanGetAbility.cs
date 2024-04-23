using _.Scripts.Enemy;
using _.Scripts.Player.Ability;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace @_.Scripts.BT
{
    public class SetCanGetAbility : EnemyAction
    {
        public bool canGet;

        public override void OnStart()
        {
            AbilityContainer container = gameObject.GetComponent<AbilityContainer>();
            if (container != null)
                container.SetCanGetAbility(canGet);
            else Debug.Log($"{this.gameObject.name} object is not AbilityContainer");
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        public override void OnEnd()
        {
        }
    }
}