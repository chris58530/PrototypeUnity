using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy;
using _.Scripts.Player.Ability;
using UnityEngine;

public class AutoTurnAroundDetect : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private List<AbilityContainer> containers = new List<AbilityContainer>();

    public static Action<GameObject> onRemoveDetectList;

    private float _detecedDistance = 13f;

    private void OnEnable()
    {
        onRemoveDetectList += RemoveDetectList;
        StartCoroutine(CheckDistanceCoroutine());
    }

    private void OnDisable()
    {
        onRemoveDetectList -= RemoveDetectList;
        StopCoroutine(CheckDistanceCoroutine());
    }

    private IEnumerator CheckDistanceCoroutine()
    {
        while (true)
        {
            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                float distance = Vector3.Distance(transform.position, enemyList[i].transform.position);

                if (distance > _detecedDistance)
                {
                    enemyList.RemoveAt(i);

                    Debug.Log(
                        $"清除 enemyList 距離:" + distance);
                }
            }

            for (int i = containers.Count - 1; i >= 0; i--)
            {
                float distance = Vector3.Distance(transform.position, containers[i].transform.position);
                if (distance > _detecedDistance)
                {
                    containers.RemoveAt(i);
                    Debug.Log(
                        $"清除 containers 距離:" + distance);
                }
            }

            yield return new WaitForSeconds(1f); // Wait for 1 second
        }
    }


    void RemoveDetectList(GameObject obj)
    {
        foreach (Enemy enemy in enemyList)
        {
            if (enemy.gameObject == obj)
            {
                enemyList.Remove(enemy);
                break;
            }
        }

        foreach (AbilityContainer container in containers)
        {
            if (container.gameObject == obj)
            {
                containers.Remove(container);
                break;
            }
        }
    }

    //回傳在 list 距離 playerTrans 最近的Enemy
    public Vector3 NearEnemy(Transform playerTrans)
    {
        if (enemyList == null) return playerTrans.position;

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

        if (nearestEnemy == null) return playerTrans.position;

        var enemyPosition = nearestEnemy.position;
        Vector3 dir = new Vector3(enemyPosition.x, playerTrans.position.y, enemyPosition.z);
        return dir;
    }

    public Vector3 NearContainers(Transform playerTrans)
    {
        if (containers == null) return playerTrans.position;


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

        if (nearContainers == null) return playerTrans.position;

        var containersPosition = nearContainers.position;
        Vector3 dir = new Vector3(containersPosition.x, playerTrans.position.y, containersPosition.z);
        return dir;
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
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            if (enemyList.Contains(enemy))
            {
                enemyList.Remove(enemy);
            }
        }

        if (other.TryGetComponent<AbilityContainer>(out var container))
        {
            if (containers.Contains(container))
            {
                containers.Remove(container);
            }
        }
    }
}