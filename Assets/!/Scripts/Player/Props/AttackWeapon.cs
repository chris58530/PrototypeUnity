using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Player;
using TMPro;
using UnityEngine;
using UniRx;

public class AttackWeapon : Weapon
{
    [SerializeField,Header("Put the sword model to here")] private GameObject swordTransform;
    
    private void Start()
    {
        transform.parent = swordTransform.transform;
        transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
        if (other.gameObject.layer != 7) return;

        damageObj.OnTakeDamage(attackValue);
        Debug.Log("攻擊了 : " + attackValue);
        //dubug
        TMP_Text t = GameObject.Find("AttackValueText").GetComponent<TMP_Text>();
        t.text = (attackValue).ToString();
    }
}