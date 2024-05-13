using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailwayFance : MonoBehaviour
{
    [SerializeField] private Animator fanceAnimator;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss2_Cart_Rhino"))
        {
            Debug.Log("Fance collided with enemy");
            fanceAnimator.Play("UP");
        }
    }
}