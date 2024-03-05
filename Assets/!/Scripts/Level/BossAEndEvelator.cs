using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using UnityEngine;

public class BossAEndEvelator : MonoBehaviour
{
    [SerializeField] private GameObject evelator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveDownCoroutine(5));
            other.transform.parent = transform;
            GameManager.Instance.SwitchScene(4);
        }
    }

    private IEnumerator MoveDownCoroutine(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            evelator.transform.position += new Vector3(0, -8 * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}