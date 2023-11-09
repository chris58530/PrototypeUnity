using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTempCold : MonoBehaviour
{
    [SerializeField] private float maxHp;
    private float _currentHp;

    [SerializeField] Slider hpSlider;

    private void Start()
    {
        _currentHp = maxHp;
    }

    public void OnTakeDamage(int value)
    {
        AudioManager.Instance.PlaySFX("EnemyInjured");
        _currentHp -= value;
        hpSlider.value = (float)(_currentHp / maxHp);
        if (_currentHp <= 0) OnDied();
    }

    public void OnDied()
    {
        Destroy(gameObject);
    }
}
