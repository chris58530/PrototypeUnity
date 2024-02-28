using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour, ITaskResult
{
    private void Start()
    {
        this.tag = "KeyDoor";
    }

    public void DoResult()
    {
        Debug.Log("Key Door Open");
        gameObject.SetActive(false);
    }
}