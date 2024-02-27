using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _.Scripts.Task
{
    public class TaskManager : MonoBehaviour
    {
        public static Action checkTaskAction;

        [SerializeField] private Task[] task;

        private void OnEnable()
        {
            checkTaskAction += Check;
        }

        private void OnDisable()
        {
            checkTaskAction -= Check;
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.I))
            // {
            //     foreach (var t in task)
            //     {
            //         Debug.Log($"任務 {t.name} 已完成)");
            //         foreach (var results in t.taskResults)
            //         {
            //             if(results.TryGetComponent<ITaskResult>(out var r))
            //             {
            //                 r.DoResult();
            //             }
            //         }
            //     }
            // }
        }

        public void Check()
        {
            foreach (var t in task)
            {
                int doneCount = 0;
                int taskObjectsCount = t.taskObjects.Length;

                foreach (var taskObj in t.taskObjects)
                {
                    if (taskObj.isDone)
                    {
                        doneCount++;
                    }
                }

                if (doneCount >= taskObjectsCount)
                {
                    Debug.Log($"任務 {t.name} 已完成)");
                    foreach (var results in t.taskResults)
                    {
                        if(results.TryGetComponent<ITaskResult>(out var r))
                        {
                            r.DoResult();
                        }
                    }
                }else    Debug.Log($"任務 {t.name} 還沒完成)");
            }
        }
    }
}