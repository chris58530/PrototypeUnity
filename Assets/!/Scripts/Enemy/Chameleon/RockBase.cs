using System;
using _.Scripts.Enemy;
using _.Scripts.Interface;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class RockBase : Enemy, IDamageable,IShieldable
{
    [Tooltip("GROUND OR PLANE MUST BE SET GROUMD LAYER")]
    [SerializeField] private bool isShield = true;
    public Image hpImage;

    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();

    private void Start()
    {
        Initialize();
        _currentHp.Subscribe(_ => { hpImage.fillAmount = _currentHp.Value / maxHp; }).AddTo(this);
    }

    void Initialize()
    {
        _currentHp.Value = maxHp;
    }

    public void OnTakeDamage(int value)
    {
        if(isShield)return;
        
        bt.SendEvent("GetHurt");
        _currentHp.Value -= value;

        if (_currentHp.Value <= 0) OnDied();
    }
    public void OnDied()
    {
        bt.SendEvent("OnDied");
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            bt.SendEvent("OnStun");
            Debug.Log($"{this.name} get stun");
        }
    }

    public void OnTakeShield(int removeValue)
    {
        isShield = false;
    }
}