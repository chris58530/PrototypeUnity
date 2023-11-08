using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
   public Animator ani;

   private void Awake()
   {
      ani = GetComponentInChildren<Animator>();
   }
}
