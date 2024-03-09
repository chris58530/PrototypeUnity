using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using _.Scripts.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneDoor : MonoBehaviour
{
    [SerializeField] private int number;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SwitchScene(number,0);
        }
    }
}