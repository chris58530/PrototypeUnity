using System;
using _.Scripts.Enemy;
using _.Scripts.Interface;
using BehaviorDesigner.Runtime;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class SlimeBase :  MonoBehaviour, IDamageable, IShieldable
{
    [Tooltip("GROUND OR PLANE MUST BE SET GROUMD LAYER")] [SerializeField]
    private bool isShield = true;


    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onStunEvent;
    [SerializeField] private UnityEvent onDiedEvent;
    private ShieldUI _shieldUI;
    [SerializeField]private Animator _animator;

   void Awake()
    {
     
        _shieldUI = GetComponentInChildren<ShieldUI>();
    }


    void Start()
    {
        _currentHp.Value = maxHp;
    }

    public void OnTakeDamage(int value)
    {
        _animator.Play("Hurt");

        Debug.Log("take damage");
        onTakeDamagedEvent?.Invoke();
        if (isShield)
        {
            _shieldUI.HitShield(1);
            return;
        }
        // AudioManager.Instance.PlaySFX("MobInjured");


        if (_currentHp.Value <= 0)
        {
            OnDied();
            return;
        }

        _currentHp.Value -= value;
        if (_currentHp.Value <= 0)
        {
            onStunEvent?.Invoke();
        }
    }

    public void OnDied()
    {
        GetComponent<Collider>().enabled = false;
        onDiedEvent?.Invoke();
        _animator.Play("Die");
        Destroy(gameObject,3);
    }

  


    public void OnTakeShield(int removeValue)
    {
        if (!isShield) return;

   

        _shieldUI.BreakShield(0);

        isShield = false;
    }

 
}
