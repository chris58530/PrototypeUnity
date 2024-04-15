using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy;
using UnityEngine;

public class AutoTurnAroundDetect : MonoBehaviour
{
    private List<Enemy> enemyList = new List<Enemy>();
    private List<AbilityContainer> containers = new List<AbilityContainer>();

    //回傳在 list 距離 playerTrans 最近的Enemy
    public Transform NearEnemy(Transform playerTrans)
    {
        if (enemyList == null) return null;
        
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemyList)
        {
            float distance = Vector3.Distance(enemy.transform.position, playerTrans.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }
    public Transform NearContainers(Transform playerTrans)
    {
        if (containers == null) return null;
        
        Transform nearContainers = null;
        float shortestDistance = Mathf.Infinity;

        foreach (AbilityContainer container in containers)
        {
            float distance = Vector3.Distance(container.transform.position, playerTrans.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearContainers = container.transform;
            }
        }

        return nearContainers;
    }

    //如果 other layer 是 Enemy 加入

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            if (enemyList.Contains(enemy)) return;
            enemyList.Add(enemy);
        }
        if (other.TryGetComponent<AbilityContainer>(out var container))
        {
            if (containers.Contains(container)) return;
            containers.Add(container);
        }
    }

    //如果 other layer 是 Enemy 踢出
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy)) {

            if (!enemyList.Contains(enemy)) return;
            enemyList.Remove(enemy);
        }
        if (other.TryGetComponent<AbilityContainer>(out var container)) {

            if (!containers.Contains(container)) return;
            containers.Remove(container);
        }
    }

}