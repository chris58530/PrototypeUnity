using System;
using System.Collections.Generic;
using UnityEngine;

namespace _.Scripts.Player
{
    public class AttackDetect : MonoBehaviour
    {
        private readonly List<GameObject> _damageObj = new List<GameObject>();
        public List<GameObject> DamageObj => _damageObj;

        public Vector3 GetAttackTarget()
        {
            float closestDistance = 100;
            var targetPos = Vector3.zero;
            foreach (var target in _damageObj)
            {
                if (target.gameObject == null) _damageObj.Remove(target);
                var distance = Vector3.Distance(target.transform.position, transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetPos = target.transform.position;
                }
            }

            return targetPos;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<IDamageable>(out var damageable)) return;
            if (other.gameObject.layer != 7) return;
            if (_damageObj.Contains(other.gameObject)) return;
            _damageObj.Add(other.gameObject);

            Debug.Log($"AttackDetect : {other.gameObject.name}");
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.TryGetComponent<IDamageable>(out var damageable)) return;
            _damageObj.Remove(other.gameObject);
        }
    }
}