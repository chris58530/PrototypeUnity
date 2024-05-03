using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackUI : MonoBehaviour
{
    [SerializeField] private GameObject trackObject;
    [SerializeField]private Vector3 offset;

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(trackObject.transform.position) + offset;
    }
}