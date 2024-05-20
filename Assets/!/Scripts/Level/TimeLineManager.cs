using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace @_.Scripts.Level
{
    public class TimeLineManager : _.Scripts.Tools.Singleton<TimeLineManager>
    {
        [SerializeField] private PlayableDirector[] playableDirectors;
        [SerializeField] public PlayableDirector currentDirector;


        public static Action onPlayTimelLine;
        public static Action onQuitTimelLine;

        private bool _isExecuteQuitAction;
        [SerializeField] private int currentActiveDirectorNumber = 0; //忘記這是幹啥的了
        private bool _isPauseTimeLine;

        private IMarker[] _stopMarkers;

        private UIInput _uiInput;


        protected override void Awake()
        {
            base.Awake();
            _uiInput = FindObjectOfType<UIInput>();
        }

        private void Update()
        {
            if (currentDirector == null) return;
            ContinueTimeline();
            SpeedUpDirectors();
        }

        public void PlayTimeLine(int num)
        {
            //Insure current time line is follow the flow
            // if (num != currentActiveDirectorNumber) return;
            Debug.Log($"play {num} timeline");
            currentActiveDirectorNumber++;
            onPlayTimelLine?.Invoke();
            currentDirector = playableDirectors[num];
            var timelineAsset = currentDirector.playableAsset as TimelineAsset;
            if (timelineAsset.markerTrack != null)
            {
                _stopMarkers = timelineAsset.markerTrack.GetMarkers().ToArray();
            }

            currentDirector.Play();
            _isExecuteQuitAction = false;

            currentDirector.stopped += QuitTimeLineDetect;
        }

        private void QuitTimeLineDetect(PlayableDirector playableDirector)
        {
            if (currentDirector == null) return;
            if (currentDirector.duration <= currentDirector.time && _isExecuteQuitAction) return;
            // Debug.Log("QuitTimeLineDetect");
            onQuitTimelLine?.Invoke();
            _isExecuteQuitAction = true;
            currentDirector = null;
        }

        private void SpeedUpDirectors()
        {
            if (_isPauseTimeLine) return;
            if (_isExecuteQuitAction) return;
            if (_stopMarkers == null) return;
            // if (currentDirector.duration - 1.5f <= currentDirector.time) return;

            if (_uiInput.Confirm)
            {
                // currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(10f);
                //markers 創建順序會影響array的順序
                //需要從第一個點位開始創建

                for (int s = 0; s < _stopMarkers.Length; s++)
                {
                    if (currentDirector.time < _stopMarkers[s].time)
                    {
                        currentDirector.time = _stopMarkers[s].time;
                        return;
                    }
                }
            }

            else currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1f);
        }

        void ContinueTimeline()
        {
            if (_uiInput.Confirm)
            {
                currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1f);
                // currentDirector.Resume();
                _isPauseTimeLine = false;
                // Debug.Log("time line continue");
            }
        }

        public void StopTimeLine()
        {
            if (currentDirector == null) return;
            _isPauseTimeLine = true;
            // currentDirector.Pause();
            // Debug.Log("Pause");

            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(0f);
        }


        private void OnEnable()
        {
        }
    }
}