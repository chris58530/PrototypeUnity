using System;
using _.Scripts;
using _.Scripts.Player;
using UnityEngine;

public class BossAEndEvelatorBlockRollArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var controller))
        {
            controller.blockRoll = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var controller))
        {
            controller.blockRoll = false;
        }
    }
}