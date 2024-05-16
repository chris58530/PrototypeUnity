using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy.BossA;
using UnityEngine;

public class BossADamage : MonoBehaviour
{
    [SerializeField] BossABase boss;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            boss.OnTakeDamage(50, Vector3.zero, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            boss.OnTakeShield(1);
        }
    }
}