using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class GlobleValues : MonoBehaviour
{
    [SerializeField] private SharedGameObject player;
    void Start()
    {
        GlobalVariables.Instance.SetVariable("Player", player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
