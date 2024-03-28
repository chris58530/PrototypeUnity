using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _.Scripts.Task;


public class TaskObject : MonoBehaviour, ITaskObject
{
    public bool isDone { get; set; }
    [SerializeField] private int taskNumber;
    [SerializeField] private bool autoSucces;

    private void OnTriggerEnter(Collider other)
    {
        if (!autoSucces) return;
        if (!other.gameObject.CompareTag("Player")) return;

        SetIsDone();

        Destroy(gameObject);
    }

    public void SetIsDone()
    {
        isDone = true;
        TaskManager.checkTaskAction?.Invoke(taskNumber);
    }
}