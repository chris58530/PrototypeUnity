using UnityEngine;
using UnityEngine.Serialization;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp;
    [SerializeField] private Animator objectAni;
    [SerializeField] private Collider objectCollider;


    public void OnTakeDamage(int value)
    {
        hp -= 1;
        if (hp <= 0) OnDied();
    }

    public void OnDied()
    {
        if (objectAni != null)
        {
            objectAni.Play("Die");
            Destroy(gameObject, 3);
            return;
        }

        objectCollider.enabled = false;
        Destroy(gameObject);
    }
}