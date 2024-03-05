using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class GlobleValues : MonoBehaviour
{
    [SerializeField] private SharedGameObject player;
    [SerializeField] private SharedBool canfight = true;
    void Start()
    {
        GlobalVariables.Instance.SetVariable("Player", player);

    }

    
}
