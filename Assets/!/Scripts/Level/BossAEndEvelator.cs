using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using _.Scripts.Player;
using UnityEngine;

public class BossAEndEvelator : MonoBehaviour
{
    [SerializeField] private GameObject evelator;
    private bool _isEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (_isEntered) return;
        if (other.TryGetComponent<PlayerInput>(out var input))
        {
            _isEntered = true;
            Debug.Log("enter evelator");
            input.enabled = false;
            StartCoroutine(MoveDownCoroutine(5));
            other.transform.parent = transform;
            GameManager.Instance.SwitchScene(4,2);
        }
    }

    private IEnumerator MoveDownCoroutine(float duration)
    {
        yield return new WaitForSeconds(1);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            evelator.transform.position += new Vector3(0, -8 * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}