using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//使用方法:將此腳本掛在遮罩上(你想要讓東西變透明的區域)以及套用HiddenMask(會讓此物件變透明)
//然後將你想遮蔽的物件掛入到此腳本的list中
public class HiddenMask : MonoBehaviour
{
    public GameObject[] ObjMasked;
    void Start()
    {
        for (int i = 0; i < ObjMasked.Length; i++)
        {
            ObjMasked[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }

}
