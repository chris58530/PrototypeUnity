using System;
using _.Scripts.Event;
using UniRx;
using UnityEngine;

namespace _.Scripts.Player
{
    public class UltimateSystem : PlayerAttackSystem
    {
        public bool finishUltimate = false;
        public float ultimateTime;
        public float chanceTime;
        public int ultimateCount = 0;
        [SerializeField] private GameObject ultimateweapon;

        public bool CanDoUltimate => playerBase.currentSkillValue.Value >= 3;


        public void UseUltimate()
        {
            Debug.Log("UltiAttack");

            ResetChance();

            ultimateCount += 1;
            playerBase.currentSkillValue.Value = 0;
            transform.LookAt(GetDirection());
            //加入位移
            ultimateweapon.SetActive(true);
        }

        public void UseFinalUltimate()
        {
            Debug.Log("UltiFinalAttack");

            chanceDisposable?.Dispose();
            finishUltimate = true;

            transform.LookAt(GetDirection());
            ultimateweapon.SetActive(true);
            ultimateCount = 0;
        }

        public void StartChanceTime()
        {
            chanceDisposable = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime)).First().Subscribe(_ =>
            {
                finishUltimate = true;
            });
        }
    }
}