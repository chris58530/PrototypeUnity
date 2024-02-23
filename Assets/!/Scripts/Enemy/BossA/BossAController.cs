using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using _.Scripts.Interface;
using UniRx;
using UniRx.Triggers;
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

        [Tooltip("Boss stand on tower and become tower's child")] [SerializeField]
        private Transform towerPoint;

        [SerializeField] private GameObject bigExplode;

        //生成物件類別
        [Header("Preview Setting")] //.
        [SerializeField]
        private GameObject previewObject;

        [Header("Juggle Bomb Setting")] //.
        [SerializeField]
        private GameObject[] juggleBomb;

        [Header("Throw Small Bomb Setting")] //.
        [SerializeField]
        private GameObject smallBomb;

        [SerializeField] private Transform smallBombPoint;

        [Header("ThrowBomb Setting")] //.
        [SerializeField]
        private GameObject bomb;

        [Header("Shake Tail Setting")] //.
        [SerializeField]
        private AnimationCurve jumpToPlayerCurve;

        [SerializeField] private GameObject[] damageCollider;

        public void ResetShield()
        {
            //do somthing
        }

        public void RemoveShield()
        {
        }

        public void OpenAttack()
        {
            for (int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].tag = "Enemy";
            }
        }

        public void CloseAttack()
        {
            for (int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].tag = "Default";
            }
        }

        public void ThrowJuggleBomb(Vector3 target)
        {
            target.x += Random.Range(-10, 10);
            target.z += Random.Range(-10, 10);
            int randomInt = Random.Range(0, juggleBomb.Length);
            var obj = Instantiate(juggleBomb[randomInt], target + new Vector3(0, 50, 0),
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
            var pos = smallBombPoint.position;
            var obj = Instantiate(smallBomb, pos, transform.rotation);
            Rigidbody objRB = obj.GetComponent<Rigidbody>();

            Destroy(obj, 3);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                objRB.velocity = objRB.transform.forward * 50;
                // obj.transform.position = Vector3.MoveTowards(obj.transform.position, offset, 300 * Time.deltaTime);
            }).AddTo(obj);
        }

        public void PreviewThrow(Transform target)
        {
            var obj = Instantiate(previewObject, transform.position, Quaternion.Euler(90, 0, 0));
            Destroy(obj, 6);
            var track = Observable.EveryUpdate().Subscribe(_ =>
            {
                obj.transform.position =
                    Vector3.MoveTowards(obj.transform.position, target.position, 80 * Time.deltaTime);
            }).AddTo(obj);
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(4)).Subscribe(_ => { track.Dispose(); }).AddTo(obj);
        }

        public void ThrowBomb(Vector3 target)
        {
            var obj = Instantiate(bomb, target + new Vector3(0, 100, 0),
                Quaternion.Euler(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360)));
            Destroy(obj, 3);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, 50 * Time.deltaTime);
            }).AddTo(obj);
        }

        public void RaiseTheTower(bool isRaise)
        {
            if (isRaise)
            {
                tower.SetActive(true);
                transform.parent = tower.transform;
                tower.GetComponent<Animator>().Play("RaiseTower");
                tower.GetComponent<BoxCollider>().enabled = true;
            }else
            tower.GetComponent<Animator>().Play("DropTower");

        }


        private void OnTriggerEnter(Collider other)
        {
            if (transform.CompareTag("Default")) return;
            if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
            if (other.gameObject.layer != 6) return;

            damageObj.OnTakeDamage(10);

            Debug.Log($"{other.name} get {10} damage");
        }
    }
}