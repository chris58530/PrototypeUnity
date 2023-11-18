using System;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace _.Scripts.Enemy
{
    public class Enemy : MonoBehaviour, IMarkable
    {
        private BehaviorTree _bt;
        private Rigidbody _rb;

        [SerializeField] private GameObject markObject;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _bt = GetComponent<BehaviorTree>();
        }

         

        private IDisposable _markRoutine;
        public bool GetMark { get; set; }

        public void Mark()
        {
            _markRoutine?.Dispose();
            GetMark = true;
            markObject.SetActive(true);
            _markRoutine = Observable.EveryUpdate()
                .Delay(TimeSpan.FromSeconds(5))
                .First()
                .Subscribe(_ => { markObject.SetActive(false); }).AddTo(this);
        }
    }
}