using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using _.Scripts.Temporary;
using UnityEngine;

public class DashAniEvent : MonoBehaviour
{
    public void Attack()
    {
        Debug.Log("attack");
        PlayerActions.onPlayerAttack?.Invoke();
    }
}