using System;
using UnityEngine;

namespace @_.Scripts.Event
{
    public static class SystemActions
    {
        public static Action<float> onSwitchScene;
        public static Action onSceneStart;
        public static Action onPlayerRespawn;
        public static Action<float, float, float> onGamePadVibrate;
        public static Action onCameraShake;
        public static Action<float> onFrameSlow;
    }
}