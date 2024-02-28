using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Task;
using UnityEngine;

public class Key : MonoBehaviour, ITaskObject
{
    public bool isDone { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KeyDoor"))
        {
            isDone = true;
            TaskManager.checkTaskAction?.Invoke(1);
            Destroy(gameObject);
        }
    }
}