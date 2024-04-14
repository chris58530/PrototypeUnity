using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class Torch : MonoBehaviour
{
    [SerializeField] private GameObject[] openFire;
    [SerializeField] private GameObject golbinPrefab;

    [SerializeField] private bool isSpawnGlobin;

    public void OpenTorchLight()
    {
        foreach (var fire in openFire)
        {
            fire.SetActive(true);
        }
    }

    public void CloseTorchLight()
    {
        foreach (var fire in openFire)
        {
            fire.SetActive(false);
        }

        if (!isSpawnGlobin)
            Instantiate(golbinPrefab, transform.position + Vector3.up * 5, transform.rotation);

        isSpawnGlobin = true;
    }
}