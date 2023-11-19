using System;
using UniRx;
using UnityEngine;

namespace _.Scripts.Enemy.TypeA
{
    public class TypeAController : EnemyController
    {
        //生成物件類別
        [Header("Preview Setting")] //.
        [SerializeField]
        private GameObject previewObject;

        [Header("ThrowBomb Setting")] //.
        [SerializeField]
        private GameObject bomb;

        [Header("Shake Tail Setting")] //.
        [SerializeField]
        private AnimationCurve jumpToPlayerCurve;

        [SerializeField] private GameObject damageCollider;

        //skill one 
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
            var obj = Instantiate(bomb, transform.position + new Vector3(0, 100, 0), Quaternion.identity);
            Destroy(obj, 3);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, 100 * Time.deltaTime);
            }).AddTo(obj);
        }

        //skill two 
        public void JumpToPlayer(Transform target, float jumpTime)
        {
            float t = 0;
            var jumpAction = Observable.EveryUpdate().Subscribe(_ =>
            {
                t += 0.01f;
                float y = jumpToPlayerCurve.Evaluate(t);
                Debug.Log(y);
                Vector3 go = new Vector3(target.position.x, target.position.y + y, target.position.z);
                transform.position = Vector3.MoveTowards(transform.position,go, 50 * Time.deltaTime);
            }).AddTo(this);
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(jumpTime)).First().Subscribe(_ =>
            {
                jumpAction.Dispose();
            }).AddTo(this);
        }

        public void ShakeTail()
        {
            //play animation 
        }
    }
}