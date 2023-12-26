using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBomb : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float speed;
    private Transform target; // 目标对象
    [SerializeField] private float rotationSpeed = 2.0f; // 旋转速度

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotateToPlayer();
        MoveForward();
    }

    void RotateToPlayer()
    {
        if (target != null)
        {
            // 计算目标方向
            Vector3 targetDirection = target.position - transform.position;

            // 计算旋转的方向
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // 使用Lerp或Slerp来平滑旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else target = GameObject.FindWithTag("Player").transform;
    }

    void MoveForward()
    {
        _rb.velocity = transform.forward * speed;
    }
}