using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _.Scripts.Task;


public class TaskObject : MonoBehaviour, ITaskObject
{
    public bool isDone { get; set; }
    [SerializeField]private int taskNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        isDone = true;
        TaskManager.checkTaskAction?.Invoke(taskNumber);

        Destroy(gameObject);
    }
}