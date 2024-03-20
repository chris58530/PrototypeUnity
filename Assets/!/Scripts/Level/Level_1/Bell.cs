using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    [SerializeField] private Animator ani;

    private void Start()
    {
        ani = GetComponentInChildren<Animator>();
    }

    public void PlayAnimation()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            ani.Play("Ring");
            ani.CrossFade("Ring", 0.1f);
        }
    }
}