using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackPlace : MonoBehaviour
{
    [SerializeField] private Transform _originalPoint;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = _originalPoint.position;
        }
    }
}