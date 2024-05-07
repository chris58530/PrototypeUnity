using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CuttleBullet : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask mask;


    void Update()
    {
        Vector3 velocity = transform.forward * (speed * Time.deltaTime);

        transform.position += velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<CuttleEffectPanel>().OnCuttleBulletHiiPlayer();
        }
        if ((mask & (1 << other.gameObject.layer)) == 0) return;
        Destroy(gameObject);
        Debug.Log("touch");
    }
}