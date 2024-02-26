using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace _.Scripts.Event
{
    public static class PlayerActions
    {
        public static Action onPlayerDead;
        public static Action onPlayerHurt;
        public static Action<Transform> onPlayerKnock;

        public static Action<float> onPlayerAttack;
        //第幾個、縮放大小
        public static Action<int,float> onPlayerAttackEffect;
        
        public static Action onHitEnemy;
        
        public static Action<int> onUseAbility; 
    }
}
