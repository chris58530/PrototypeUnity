using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveTowards : EnemyAction
{
    public SharedFloat speed;
    public SharedGameObject target;
    public float distance;
    private Vector3 _destination;

    public override void OnStart()
    {

        _destination = transform.position * distance;
    }

    public override TaskStatus OnUpdate()
    {
        Debug.Log(gameObject.transform.rotation);


        if (Vector3.Magnitude(transform.position - _destination) < 0.1f)
        {
            return TaskStatus.Success;
        }


        transform.position =
            Vector3.MoveTowards(transform.position, _destination, speed.Value * Time.deltaTime);


        return TaskStatus.Running;
    }
}