using System;

namespace _.Scripts.Event
{
    public static class PlayerActions
    {
        public static Action onPlayerDead;
        public static Action<float> onPlayerAttack;
        public static Action<int> onUseAbility;

    }
}
