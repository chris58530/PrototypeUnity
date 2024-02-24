using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using MagicaCloth2;
using UniRx;
using UnityEngine;
using TimeSpan = System.TimeSpan;

[TaskCategory("BossA")]
public class RotateSelf : EnemyAction
{
    public SharedGameObject rotateTarget;
    public SharedFloat rotateSpeed;
    public SharedFloat time;
    private float caculateTime;
    public SharedGameObject lookAtTarget;
    public bool lookAt;
    public float speed;

    public override void OnStart()
    {
        caculateTime = 0;
    }

    public override TaskStatus OnUpdate()
    {
        caculateTime += Time.deltaTime;
        if (caculateTime >= time.Value)
            return TaskStatus.Success;


        if (lookAt)
        {
            var targetRotation = Quaternion.LookRotation(lookAtTarget.Value.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else
        {
            float rotation = rotateSpeed.Value * Time.deltaTime;
            rotateTarget.Value.transform.Rotate(Vector3.up, rotation);
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
    }
}