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
    private PlayerUseTimeLineUI _playerUseTimeLineUI;
    private PlayerInput _input;
    private bool _canConfirm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInput>(out var input))
        {
            _input = input;
            other.transform.parent = transform;
        }

        if (!other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>()) return;
        _playerUseTimeLineUI = other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>();
        _playerUseTimeLineUI.ShowCanConfirmImage(true);

        _canConfirm = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>()) return;

        //
        // if (other.TryGetComponent<PlayerInput>(out var input))
        //     other.transform.parent = null;


        _playerUseTimeLineUI = other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>();
        _playerUseTimeLineUI.ShowCanConfirmImage(false);

        _canConfirm = false;
    }

    private void Update()
    {
        if (!_canConfirm) return;

        ConfirmTimeline();
    }

    public void ConfirmTimeline()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;
        if (_isEntered) return;
        _input.enabled = false;

        _isEntered = true;
        _playerUseTimeLineUI.ShowCanConfirmImage(false);

        GameManager.Instance.SwitchScene(4, 2);
        StartCoroutine(MoveDownCoroutine(5));
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