using System;
using _.Scripts.Event;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class PlayAudio : EnemyAction
{
    public String audioName;

    public override void OnStart()
    {
        AudioManager.Instance.PlaySFX(audioName);

    }


    public override TaskStatus OnUpdate()
    {
        // Return a task status of success once we've reached the target


        return TaskStatus.Success;
    }
}