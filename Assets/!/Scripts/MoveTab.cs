using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTab : MonoBehaviour
{
    private bool move;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player") return;
        move = true;
    }

    private void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }
    }
}