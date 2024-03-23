using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private GameObject openFire;
    [SerializeField] private GameObject golbinPrefab;

    private bool _isOpenFire;
    private IDisposable _spawnGoblinDisposable;

    public void OpenTorchLight()
    {
        openFire.SetActive(true);
        _spawnGoblinDisposable = Observable.Interval(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
        {
            Instantiate(golbinPrefab, transform.position + Vector3.up, transform.rotation);
        }).AddTo(this);
    }

    public void CloseTorchLight()
    {
        _isOpenFire = false;
        _spawnGoblinDisposable?.Dispose();
        openFire.SetActive(false);
    }

  
}