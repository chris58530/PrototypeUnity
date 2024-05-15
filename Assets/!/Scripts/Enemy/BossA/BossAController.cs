using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using _.Scripts.Interface;
using UniRx;
using UniRx.Triggers;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace @_.Scripts.Enemy.BossA
{
    public class BossAController : EnemyController
    {
        [Header("Before Big Bomb Tower")] //.
        [SerializeField]
        private GameObject tower;

        [SerializeField] private Animator towerAni;

        [Tooltip("Boss stand on tower and become tower's child")] [SerializeField]
        private Transform towerPoint;

        [SerializeField] private ParticleSystem raiseTower;
        [SerializeField] private ParticleSystem brokenTower;


        //生成物件類別
        [Header("BreakTower Setting")] //.
        [SerializeField]
        private GameObject BreakTowerPrefab;

        [SerializeField] private Transform BreakTowerPoint;

        [Header("Preview Setting")] //.
        [SerializeField]
        private GameObject previewObject;

        [Header("Juggle Bomb Setting")] //.
        [SerializeField]
        private GameObject juggleBomb;

        [Header("Throw Small Bomb Setting")] //.
        [SerializeField]
        private GameObject smallBomb;

        [SerializeField] private Transform smallBombPoint;

        [SerializeField] private ParticleSystem smallBombParticle;

        [Header("ThrowBomb Setting")] //.
        [SerializeField]
        private GameObject bomb;


        [SerializeField] private Collider[] tailDamageCollider;
        [SerializeField] private Collider[] headDamageCollider;

        public static Action onRaiseTower;
        public static Action onDownTower;


        public void OpenTailAttack(bool isOpen)
        {
            for (int i = 0; i < tailDamageCollider.Length; i++)
                tailDamageCollider[i].enabled = isOpen;
        }

        public void OpenHeadAttack(bool isOpen)
        {
            for (int i = 0; i < headDamageCollider.Length; i++)
                headDamageCollider[i].enabled = isOpen;
        }

        public void ThrowJuggleBomb(Vector3 target)
        {
            target.x += Random.Range(-10, 10);
            target.z += Random.Range(-10, 10);
            var obj = Instantiate(juggleBomb, target + new Vector3(0, 50, 0),
                Quaternion.Euler(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360)));
            Rigidbody objRB = obj.GetComponent<Rigidbody>();
            Vector3 offset = -(obj.transform.position - target).normalized;
            Destroy(obj, 3);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                objRB.velocity = offset * 35;
                // obj.transform.position = Vector3.MoveTowards(obj.transform.position, offset, 300 * Time.deltaTime);
            }).AddTo(obj);
        }

        public void ThrowSmallBomb()
        {
            var pos = smallBombPoint;
            var obj = Instantiate(smallBomb, pos.position, pos.rotation);
            Rigidbody objRB = obj.GetComponent<Rigidbody>();

            Destroy(obj, 3);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                objRB.velocity = objRB.transform.forward * 50;
                // obj.transform.position = Vector3.MoveTowards(obj.transform.position, offset, 300 * Time.deltaTime);
            }).AddTo(obj);

            //特效
            smallBombParticle.Play();
        }

        public void PreviewThrow(Transform target)
        {
            var obj = Instantiate(previewObject, target.position, Quaternion.Euler(90, 0, 0));
            Destroy(obj, 6);
            var track = Observable.EveryUpdate().Subscribe(_ =>
            {
                obj.transform.position =
                    Vector3.MoveTowards(obj.transform.position, target.position, 80 * Time.deltaTime);
            }).AddTo(obj);
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(3)).Subscribe(_ => { track.Dispose(); }).AddTo(obj);
        }

        public void ThrowBomb(Vector3 target)
        {
            var obj = Instantiate(bomb, target + new Vector3(0, 100, 0),
                Quaternion.identity);
            Destroy(obj, 5);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, 50 * Time.deltaTime);
            }).AddTo(obj);
        }

        public void RaiseTheTower(bool isRaise)
        {
            if (isRaise)
            {
                onRaiseTower?.Invoke();
                tower.SetActive(true);
                towerAni.Play("RaiseTower");
                Vector3 brokenTowerOffset = new Vector3(0, 15, 0);
                raiseTower.Play();
            }
            else
            {
                onDownTower?.Invoke();

                Vector3 brokenTowerOffset = new Vector3(0, 15, 0);
                GameObject breakTowerInstance =
                    Instantiate(BreakTowerPrefab, BreakTowerPoint.position, Quaternion.identity);
                Destroy(breakTowerInstance, 5f);

                // brokenTower.Play();
                towerAni.Play("DropTower");
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (transform.CompareTag("Default")) return;
            if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
            if (other.gameObject.layer != 6) return;

            damageObj.OnTakeDamage(10, Vector3.zero, quaternion.identity);

            Debug.Log($"{other.name} get {10} damage");
        }
    }
}