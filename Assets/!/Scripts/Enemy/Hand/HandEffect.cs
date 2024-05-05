using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEffect : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Material brokenMaterial1;
    [SerializeField] private Material brokenMaterial2;

    public void SwitchBreakMaterial(BreakState breakState)
    {
        if (breakState == BreakState.Break1)
            foreach (var m in renderers)
            {
                m.material = brokenMaterial1;
            }

        if (breakState == BreakState.Break2)
            foreach (var m in renderers)
            {
                m.material = brokenMaterial2;
            }
    }
}