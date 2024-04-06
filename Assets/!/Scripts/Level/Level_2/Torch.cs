using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private GameObject[] openFire;
    [SerializeField] private GameObject golbinPrefab;

    [SerializeField]private bool _isOpenFire;
    private IDisposable _spawnGoblinDisposable;

    public void OpenTorchLight()
    {
        foreach (var fire in openFire)
        {
            fire.SetActive(true);

        }
        _spawnGoblinDisposable = Observable.Interval(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
        {
            Instantiate(golbinPrefab, transform.position + Vector3.up, transform.rotation);
        }).AddTo(this);
    }

    public void CloseTorchLight()
    {
        _isOpenFire = false;
        _spawnGoblinDisposable?.Dispose();
        foreach (var fire in openFire)
        {
            fire.SetActive(false);

        }
    }

  
}