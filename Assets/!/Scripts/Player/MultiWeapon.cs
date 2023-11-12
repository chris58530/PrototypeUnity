using System.Collections;
using System.Collections.Generic;
using _.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MultiWeapon : MonoBehaviour
{
    [SerializeField]private int attackValue;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
        if (other.gameObject.layer != 7) return;

        int combo = FindObjectOfType<PlayerCombo>().combo;

        damageObj.OnTakeDamage(attackValue + combo / 2);

        //dubug
        TMP_Text t = GameObject.Find("AttackValueText").GetComponent<TMP_Text>();
        t.text = (attackValue + combo).ToString();
    }
}