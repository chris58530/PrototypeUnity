using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class HeartTest : MonoBehaviour
{
    public int health = 4;
    public int numOfHearts;

    public GameObject[] hearts;
    public Animator HeartUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage();
            HeartUI.Play("HurtShaking");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Heal();
        }
    }

    void TakeDamage()
    {
        health--;
        health = Mathf.Clamp(health, 0, numOfHearts); 
        UpdateHearts();
    }

    void Heal()
    {
        health++;
        health = Mathf.Clamp(health, 0, numOfHearts); 
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < numOfHearts; i++)
        {
            if (i < health)
            {
                hearts[i].SetActive(true);
                Transform heartTransform = hearts[i].transform.Find("Heart");
                Animator animator = heartTransform?.GetComponent<Animator>();
                animator.Play("Heal");
            }
            else
            {
                Transform heartTransform = hearts[i].transform.Find("Heart");
                Animator animator = heartTransform?.GetComponent<Animator>();

                if (animator != null)
                {
                    animator.Play("HeartBreaking");
                }          
            }
        }
    }
}
