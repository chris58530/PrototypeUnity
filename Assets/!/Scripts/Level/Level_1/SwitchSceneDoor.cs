using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using _.Scripts.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SwitchScene(1);
        }
    }
}