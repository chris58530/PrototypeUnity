using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleWeapon : MonoBehaviour
{
    [SerializeField]private int attackValue;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
        if (other.gameObject.layer != 7) return;

        int combo = FindObjectOfType<PlayerCombo>().combo;

        damageObj.OnTakeDamage(attackValue + combo );

        //dubug
        TMP_Text t = GameObject.Find("AttackValueText").GetComponent<TMP_Text>();
        t.text = (attackValue + combo).ToString();
    }
}
