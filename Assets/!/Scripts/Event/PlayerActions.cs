using System;

namespace _.Scripts.Event
{
    public static class PlayerActions
    {
        public static Action onPlayerDead;
        public static Action onPlayerAttack;
        public static Action<int> onUseAbility;

    }
}
