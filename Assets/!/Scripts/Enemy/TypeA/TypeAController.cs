using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

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
        public void JumpToPlayer(NavMeshAgent nav, Transform player, float jumpTime)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.position);
            float speed = distanceToTarget / jumpTime; // Calculate speed based on distance

            Vector3 destination = new Vector3(player.position.x, player.position.y, player.position.z);
            StartCoroutine(JumpToPlayerRoutine(destination, jumpTime, speed));
        }

        IEnumerator JumpToPlayerRoutine(Vector3 destination, float time, float speed)
        {
            float t = 0;
            while (t < time)
            {
                Debug.Log(t);
                t += Time.deltaTime;
                float y = jumpToPlayerCurve.Evaluate(t);
           
                destination.y = y;
                transform.position = Vector3.MoveTowards(transform.position, destination,
                    speed * Time.deltaTime);

                yield return null;
            }

            yield return null;
        }

        public void ShakeTail()
        {
            //play animation 
        }
    }
}