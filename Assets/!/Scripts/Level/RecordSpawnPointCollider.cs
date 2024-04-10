using System;
using UnityEngine;

public class RecordSpawnPointCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelSceneManager.Instance.currentSpawnNumber++;
            Destroy(gameObject);
        }
    }
}