using System;
using _.Scripts.Event;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace _.Scripts.Player
{
    public class UltimateSystem : PlayerAttackSystem
    {
        public bool finishUltimate = false;
        public float ultimateTime;
        public float chanceTime;
        public int ultimateCount = 0;
        [SerializeField] private GameObject ultimateweapon;
        [SerializeField] private GameObject attackChancePreview;
        public bool isFail;
        public bool CanDoUltimate => playerBase.currentSwordLevelValue.Value >= playerBase.maxSwordLevelValue;
        public bool finishUltiAttack;

        public void UseUltimate()
        {
            Debug.Log("UltiAttack");

               
            DisposeTimer();


            ultimateCount ++;
            
            //重製等級
            playerBase.currentSwordLevelValue.Value = 0;
            
            transform.LookAt(GetDirection());
            
            ultimateweapon.SetActive(true);
        }

        private IDisposable _ultiTimer;

        public void UltimateTimer(bool isCaculate)
        {
             _ultiTimer?.Dispose();

            _ultiTimer = Observable.EveryUpdate().Where(_=>isCaculate)
                .First().Delay(TimeSpan.FromSeconds(ultimateTime))
                .Subscribe(_ =>
                {
                    Debug.Log("finishUltimate = true");
                    finishUltiAttack = true;
                });
        }

        public void UseFinalUltimate()
        {
            Debug.Log("UltiFinalAttack");
            DisposeTimer();
          

            transform.LookAt(GetDirection());
            ultimateweapon.SetActive(true);
        }

        public void CancelUltimate()
        {
            ultimateweapon.SetActive(false);
        }

        public void Fail()
        {
            DisposeTimer();
            finishUltimate = true;
            ultimateCount = 0;
            
        }

        public void DisposeTimer()
        {
            chanceTimer?.Dispose();
            swordLevelTimer?.Dispose();
        }
        public void UltiChanceTimer()
        {
            chanceTimer = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime)).First().Subscribe(_ =>
            {
                Debug.Log("finishUltimate = true");
                isFail = true;
            });
        }

        public void AttackChancePreview(Color color)
        {
            attackChancePreview.GetComponent<MeshRenderer>().material.color = color;
        }
    }
}