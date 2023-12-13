using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private ITask _task;
    private ITaskResult _result;
    [SerializeField]private Dictionary<ITask, ITaskResult> taskDIC = new Dictionary<ITask, ITaskResult>();
    
}
