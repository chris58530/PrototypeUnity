using UnityEngine;
using UnityEngine.Events;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [Tooltip("負責血量控制以及呼叫onTakeDamagedEvent和onDiedEvent")]
    
    [SerializeField] private int hp;
    
    [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onDiedEvent;

    public void OnTakeDamage(int value)
    {
        onTakeDamagedEvent?.Invoke();
        hp -= 1;
        if (hp <= 0) OnDied();
    }

    public void OnDied()
    {
        onDiedEvent?.Invoke();

    }

    private void OnEnable()
    {
        BossABomb.bossABigBombEvent += OnDied;
    }

    private void OnDisable()
    {
        BossABomb.bossABigBombEvent -= OnDied;
    }
}