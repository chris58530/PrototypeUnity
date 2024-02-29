using System;
using _.Scripts.Enemy;
using _.Scripts.Interface;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RhinoBase : Enemy, IDamageable,IShieldable
{
    [Tooltip("GROUND OR PLANE MUST BE SET GROUMD LAYER")]
    [SerializeField] private bool isShield = true;
    public Image hpImage;

    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onStunEvent;
    [SerializeField] private UnityEvent onDiedEvent;

    private void Start()
    {
        Initialize();
        _currentHp.Subscribe(_ => { hpImage.fillAmount = _currentHp.Value / maxHp; }).AddTo(this);
        BossABomb.bossABigBombEvent += BossABigBombDie;
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
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            bt.SendEvent("OnStun");
            Debug.Log($"{this.name} get stun collision on{other.gameObject.name}" );
        }
    }


    public void OnTakeShield(int removeValue)
    {
        isShield = false;
    }

    public void BossABigBombDie()
    {
        // bt.SendEvent("OnDied");
        
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        BossABomb.bossABigBombEvent -= BossABigBombDie;
    }
}
