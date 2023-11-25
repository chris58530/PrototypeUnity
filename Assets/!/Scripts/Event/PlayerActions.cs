using System;
using JetBrains.Annotations;
using UnityEngine.ProBuilder;

namespace _.Scripts.Event
{
    public static class PlayerActions
    {
        public static Action onPlayerDead;
        public static Action onPlayerHurt;
        public static Action<float> onPlayerAttack;
        public static Action onHitPlayer;
        public static Action<float> onHitEnemy;
        public static Action<int> onUseAbility; 
    }
}
