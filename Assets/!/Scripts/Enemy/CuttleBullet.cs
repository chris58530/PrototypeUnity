using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CuttleBullet : MonoBehaviour
{
    private Vector3 _targetPosition;
    private GameObject _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _height; // 抛物线的高度
    private float _startTime;
    private float _journeyLength;

    void Start()
    {
        // 获取玩家的初始位置
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            _targetPosition = _player.transform.position;
            _startTime = Time.time;
            _journeyLength = Vector3.Distance(transform.position, _targetPosition);
        }
    }

    void Update()
    {
        // 计算已经过去的时间
        float distCovered = (Time.time - _startTime) * _speed;

        // 计算运动的比例
        float fracJourney = distCovered / _journeyLength;

        // 计算朝向玩家位置的方向
        Vector3 direction = (_targetPosition - transform.position).normalized;

        // 计算速度
        Vector3 velocity = direction * _speed * Time.deltaTime;

        // 更新位置
        transform.position += velocity;

        // 应用抛物线运动的高度
        float yPos = Mathf.Sin(fracJourney * Mathf.PI) * _height;
        transform.position = new Vector3(transform.position.x, _targetPosition.y + yPos, transform.position.z);

        // 如果到达目标位置，则停止移动
        if (fracJourney >= 1f)
        {
            // 可以执行到达目标位置后的操作
            Destroy(gameObject);
            enabled = false; // 停止 Update 方法的调用
        }
    }

   
}