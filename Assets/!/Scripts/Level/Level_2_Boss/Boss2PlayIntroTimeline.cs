using UnityEngine;

namespace @_.Scripts.Level.Level_2_Boss
{
    public class Boss2PlayIntroTimeline : MonoBehaviour
    {
        private void Start()
        {
            TimeLineManager.Instance.PlayTimeLine(0);
        }
    }
}