using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossBController : MonoBehaviour
{
   
    [Header("Objects")]

    [SerializeField] private GameObject[] right_AttaKWeapons;
    [SerializeField] private GameObject[] left_AttaKWeapons;
    
    


    public void AttaKWeapon_Right(bool open )
    {
   
        foreach (GameObject weapon in right_AttaKWeapons)
        {
            weapon.SetActive(open);
        }
    } public void AttaKWeapon_Left(bool open)
    {
   
        foreach (GameObject weapon in left_AttaKWeapons)
        {
            weapon.SetActive(open);
        }
    }

  
    
}