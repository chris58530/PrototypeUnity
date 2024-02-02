using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using MagicaCloth2;
using UniRx;
using UnityEngine;
using TimeSpan = System.TimeSpan;
[TaskCategory("BossA")]

public class RotateSelf : EnemyAction
{
    public SharedFloat rotateSpeed;
    public SharedFloat time;
    private float caculateTime;

    public override void OnStart()
    {
        caculateTime = 0;
    }

    public override TaskStatus OnUpdate()
    {
        caculateTime += Time.deltaTime;
        if (caculateTime >= time.Value)
            return TaskStatus.Success;

        float rotation = rotateSpeed.Value * Time.deltaTime;

        transform.Rotate(Vector3.up, rotation);


        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
    }
}