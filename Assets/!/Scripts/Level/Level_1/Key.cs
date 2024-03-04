using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Task;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using Observable = UniRx.Observable;

public class Key : MonoBehaviour, ITaskObject
{
    public bool isDone { get; set; }
    [SerializeField] private int taskNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KeyDoor"))
        {
            Debug.Log("touch door");
            isDone = true;
            TaskManager.checkTaskAction?.Invoke(taskNumber);
        }
    }
}