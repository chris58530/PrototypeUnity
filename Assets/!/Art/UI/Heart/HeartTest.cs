using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HeartTest : MonoBehaviour
{
    public int numOfHearts;

    public GameObject[] hearts;
    private Animator _ani;

    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }


    public void UpdateHearts(int hp)
    {
        _ani.Play("HurtShaking");
        for (int i = 0; i < numOfHearts; i++)
        {
            if (i < hp)
            {
                hearts[i].SetActive(true);
                Transform heartTransform = hearts[i].transform.Find("Heart");
                Animator animator = heartTransform.GetComponent<Animator>();
                animator.Play("Heal");
            }
            else
            {
                Transform heartTransform = hearts[i].transform.Find("Heart");
                Animator animator = heartTransform.GetComponent<Animator>();

                if (animator != null)
                {
                    animator.Play("HeartBreaking");
                }
            }
        }
    }
}