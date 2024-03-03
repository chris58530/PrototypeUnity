using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour, ITaskResult
{
    [SerializeField] private GameObject door;
    private void Start()
    {
        this.tag = "KeyDoor";
    }

    public void DoResult()
    {
        Debug.Log("Key Door Open");
        door.gameObject.SetActive(false);
    }
}