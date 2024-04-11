using System;
using _.Scripts;
using _.Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;

public class BossAEndEvelatorBlockRollArea : MonoBehaviour
{
    [Header("active change bright material")]
    [SerializeField] private GameObject evelator;

    [SerializeField] private Material brightMaterial;
    [SerializeField] private Material _originMaterial;
    private PlayerUseTimeLineUI _playerUseTimeLineUI;




    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var controller))
        {
            evelator.gameObject.GetComponent<MeshRenderer>().material = brightMaterial;
            evelator.GetComponent<BossAEndEvelator>().enabled = true;
            controller.blockRoll = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var controller))
        {
            evelator.gameObject.GetComponent<MeshRenderer>().material = _originMaterial;

            controller.blockRoll = false;
        }

     
    }


}