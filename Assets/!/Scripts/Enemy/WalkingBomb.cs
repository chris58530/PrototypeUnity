using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBomb : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float speed;
    [Range(0, 1)] [SerializeField] private float rotationSpeed; // 旋转速度
    private Transform _target; // 目标对象
    [SerializeField] private GameObject explosion;

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
        if (_target != null)
        {
            // 计算目标方向
            var thisPos = transform.position;
            Vector3 targetDirection = _target.position - thisPos;
            targetDirection.y = 0;
            // 计算旋转的方向
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // 使用Lerp或Slerp来平滑旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else _target = GameObject.FindWithTag("Player").transform;
    }

    void MoveForward()
    {
        _rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            GameObject explose = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explose, 0.3f);
            Destroy(gameObject);
        }
    }
}