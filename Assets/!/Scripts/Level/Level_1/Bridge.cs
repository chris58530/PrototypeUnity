using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Level;
using UnityEngine;
using UnityEngine.Serialization;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject brige;

    [SerializeField] private TaskObject[] taskObj;

    

    private void LateUpdate()
    {
        //改用事件呼叫
        CheckTask();
    }

    void CheckTask()
    {
        int checkCount = 0;
        foreach (var task in taskObj)
        {
            if (task.isDone) checkCount++;
        }

        if (checkCount >= taskObj.Length) OpenBridge();
    }

    void OpenBridge()
    {
        brige.GetComponent<Animator>().Play("PutDownBridge");
    }
}