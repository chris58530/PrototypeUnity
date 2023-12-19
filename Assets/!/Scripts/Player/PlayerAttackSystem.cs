using System;
using UnityEngine;

namespace _.Scripts.Player
{
    public  class PlayerAttackSystem: MonoBehaviour
    {
        protected IDisposable chanceDisposable;
        protected IDisposable lastAttack;
    
        protected PlayerBase playerBase;
        protected PlayerSword playerSword;
        protected virtual void Awake()
        {
            playerBase = GetComponent<PlayerBase>();
            playerSword = GetComponentInChildren<PlayerSword>();
        }
        protected Vector3 GetDirection()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var layerMask = 1 << 9;
            RaycastHit hit;
            var hitpoint = Vector3.zero;
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                hitpoint = hit.point;
                hitpoint.y = transform.position.y;
                return hitpoint;
            }
            return hitpoint;
        }
        protected void DecreaseSkill() //Invoke Mathod
        {
            if (playerBase.currentSwordLevelValue.Value > 0)
            {
                playerBase.currentSwordLevelValue.Value -= 1;
            }
        }

        protected void ResetSkill()
        {
            playerBase.currentSwordLevelValue.Value = 0;
        }
        public void ResetChance()
        {

            lastAttack?.Dispose();
            chanceDisposable?.Dispose();
        }
        private void OnDisable()
        {
            if (IsInvoking(nameof(DecreaseSkill))) CancelInvoke(nameof(DecreaseSkill));
        }
    }
}