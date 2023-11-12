using System;
using _.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace _.Scripts.Temporary
{
    public class BeatManager : Singleton<BeatManager>
    {
        public static bool onBeat;
        public static bool missBeat;

        [Header("拍子間隔")] public float beatTimeInterval;
        private float _currentBeatTimeInterval;

        [Header("拍子點緩衝範圍")] [SerializeField, Range(0.1f, 1)]
        private float beatPointRange;

        //debug
        private float checktime;

        private IDisposable _disposeBetPointRange;

        [SerializeField] private Image _readyToDashImage;

        // private void Start()
        // {
        //     _currentBeatTimeInterval = beatTimeInterval;
        //     // Observable.EveryUpdate().Sample(TimeSpan.FromSeconds(beatTimeInterval)).Subscribe(_ =>
        //     // {
        //     //  
        //     // }).AddTo(this);
        //
        //
        //     Observable.EveryUpdate()
        //         .Where(_ => onBeat)
        //         .Subscribe(_ => { checktime += Time.deltaTime; }).AddTo(this);
        // }
        //
        // private void Update()
        // {
        //     _currentBeatTimeInterval -= Time.deltaTime;
        //     if (_currentBeatTimeInterval <= 0)
        //     {
        //         Debug.Log("ON BEAT");
        //         AudioManager.Instance.PlaySFX("Bom");
        //         onBeat = true;
        //         _readyToDashImage.enabled = true;
        //         _currentBeatTimeInterval = beatTimeInterval;
        //         _disposeBetPointRange = Observable.EveryUpdate()
        //             .Where(_ => onBeat)
        //             .First()
        //             .Sample(TimeSpan.FromSeconds(beatPointRange))
        //             .Subscribe(_ =>
        //             {
        //                 Debug.Log("miss beat");
        //                 // Debug.Log(checktime);
        //                 checktime = 0;
        //                 missBeat = true;
        //                 onBeat = false;
        //                 _readyToDashImage.enabled = false;
        //             }).AddTo(this);
        //     }
        // }
    }
}