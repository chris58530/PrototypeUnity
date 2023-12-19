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
        [SerializeField] private GameObject attackChancePreview;

        public bool CanDoUltimate => playerBase.currentSwordLevelValue.Value == playerBase.maxSwordLevelValue;


        public void UseUltimate()
        {
            Debug.Log("UltiAttack");

            ResetChance();

            ultimateCount += 1;
            playerBase.currentSwordLevelValue.Value = 0;
            transform.LookAt(GetDirection());
            //加入位移
            ultimateweapon.SetActive(true);
        }

        public void UseFinalUltimate()
        {
            Debug.Log("UltiFinalAttack");

            chanceDisposable?.Dispose();

            transform.LookAt(GetDirection());
            ultimateweapon.SetActive(true);
            ultimateCount = 0;
        }

        public void CancelUltimate()
        {
            ultimateweapon.SetActive(false);

        }

        public void StartChanceTime()
        {
            chanceDisposable = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime)).First().Subscribe(_ =>
            {
                finishUltimate = true;
            });
        }
        public void AttackChancePreview(Color color)
        {
            attackChancePreview.GetComponent<MeshRenderer>().material.color = color;
        }
    }
}