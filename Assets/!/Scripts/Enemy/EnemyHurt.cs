using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHurt : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHp;
    private float _currentHp;

    [SerializeField] private Image hpImage;

    private void Start()
    {
        _currentHp = maxHp;
    }

    public void OnTakeDamage(int value)
    {
        AudioManager.Instance.PlaySFX("EnemyInjured");
        _currentHp -= value;
        hpImage.fillAmount = (float)(_currentHp / maxHp);
        if (_currentHp <= 0) OnDied();
    }

    public void OnDied()
    {
        Destroy(gameObject);
    }
}