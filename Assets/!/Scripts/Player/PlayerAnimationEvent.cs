using System.Collections;
using System.Collections.Generic;
using _.Scripts.Player.Props;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public void OpenWeaponCollider()
    {
        AttackSystem.openWeaponCollider?.Invoke(true);
    }
    public void OpenQ3WeaponCollider()
    {
        AttackSystem.openQ3WeaponCollider?.Invoke(true);

    }
}