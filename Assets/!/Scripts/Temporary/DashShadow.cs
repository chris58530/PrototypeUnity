using System;
using System.Collections;
using UnityEngine;
using UniRx;

namespace _.Scripts.Temporary
{
    public class DashShadow : MonoBehaviour
    {
        private float _waitTime;
        private Transform _target;

        public void Init(float waitTime, Transform target)
        {
            _waitTime = waitTime;
            _target = target;
            StartCoroutine(MoveToPlayer());
        }

        IEnumerator MoveToPlayer()
        {
            yield return new WaitForSeconds(_waitTime - 0.7f);

            float t = 0;
            while (true)
            {
                t += Time.deltaTime;
                float a = t / 0.7f;
                transform.position = Vector3.Lerp(transform.position, _target.position, a);
                if (a >= 1f)
                {
                    Destroy(gameObject);
                    break;
                }

                yield return null;
            }
        }

        private bool frist;

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}