using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy;
using UnityEngine;

public class CartRhinoBase : Enemy
{
    [SerializeField] private GameObject rhinoModle;
    public bool isGetCatch;
    public void CatchRhino()
    {
        isGetCatch = true;
       rhinoModle.SetActive(false);
    }
}
