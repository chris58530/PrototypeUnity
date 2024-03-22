using System;
using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(DamageableObject))]
public class Crystal : MonoBehaviour
{
    [SerializeField] private GameObject detroyCrystalObject;
    [SerializeField] private Vector3 detroyCrystalObjectOffset;
    [SerializeField] private ParticleSystem absortObject;
    [SerializeField] private GameObject modelObject;

    public bool canRelife;

    [SerializeField] private float relifeTime;

    public void OnDied()
    {
        BossABomb.bossABigBombEvent -= OnDied;

        modelObject.SetActive(false);

        GameObject obj = Instantiate(detroyCrystalObject, transform.position + detroyCrystalObjectOffset,
            transform.rotation);
        Destroy(obj, 3);
        GetComponent<Collider>().enabled = false;
        if (canRelife) ReLife();
    }

    public void OnAbosrt()
    {
        modelObject.SetActive(false);

        absortObject.Play();

        GetComponent<Collider>().enabled = false;
        if (canRelife) ReLife();
        return;
        Destroy(gameObject, 3);
    }

    public void ReLife()
    {
        Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(relifeTime)).Subscribe(_ =>
            {
                modelObject.SetActive(true);

                GetComponentInChildren<Animator>().Play("Grow");
            })
            .AddTo(this);
        Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(relifeTime + 1)).Subscribe(_ =>
            {
                GetComponent<Collider>().enabled = true;
            })
            .AddTo(this);
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