using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _.Scripts.Task
{
    public class TaskManager : MonoBehaviour
    {
        public static Action<int> checkTaskAction;

        [SerializeField] public Task[] task;

        private void OnEnable()
        {
            checkTaskAction += Check;
        }

        private void OnDisable()
        {
            checkTaskAction -= Check;
        }

        public void Check(int taskCount)
        {
            int doneCount = 0;
            Task t = task[taskCount];
            
            int taskObjectsCount = t.taskObjects.Length;
          

            foreach (var taskObj in  t.taskObjects)
            {
                if (taskObj.TryGetComponent<ITaskObject>(out var obj))
                {
                    if (obj.isDone)
                        doneCount++;
                }else Debug.Log($"物件 {taskObj.name} 不是任務物件)");
            }
            
            if (doneCount >= taskObjectsCount)
            {
                Debug.Log($"任務 {t.name} 已完成)");
                foreach (var results in t.taskResults)
                {
                    if (results.TryGetComponent<ITaskResult>(out var r))
                    {
                        r.DoResult();
                    }else Debug.Log($"物件 {results.name} 不是任務物件)");
                }
            }
            else Debug.Log($"任務 {t.name} 還沒完成)");
        }
    }
}