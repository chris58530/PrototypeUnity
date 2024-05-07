using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HandEffect : MonoBehaviour
{
    //模型總共有三個 依序開啟
    [SerializeField] private GameObject[] originObjects;
    [SerializeField] private GameObject[] break1Objects;
    [SerializeField] private GameObject[] break2Objects;


    public void SwitchBreakMaterial(BreakState breakState)
    {
        if (breakState == BreakState.Break1)
        {
            //除了break1Objects都關掉 break1Objects打開
            for (int i = 0; i < originObjects.Length; i++)
            {
                originObjects[i].SetActive(false);
            }

            for (int i = 0; i < break1Objects.Length; i++)
            {
                break1Objects[i].SetActive(true);
            }

            for (int i = 0; i < break2Objects.Length; i++)
            {
                break2Objects[i].SetActive(false);
            }
        }

        if (breakState == BreakState.Break2)
        {
            for (int i = 0; i < originObjects.Length; i++)
            {
                originObjects[i].SetActive(false);
            }

            for (int i = 0; i < break1Objects.Length; i++)
            {
                break1Objects[i].SetActive(false);
            }

            for (int i = 0; i < break2Objects.Length; i++)
            {
                break2Objects[i].SetActive(true);
            }
        }
    }
}