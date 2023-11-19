using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
   [HideInInspector]public Animator ani;

   private void Awake()
   {
      ani = GetComponentInChildren<Animator>();
   }
}
