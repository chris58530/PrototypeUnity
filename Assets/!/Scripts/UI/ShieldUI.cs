using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Animator[] shieldAni;
    private Animator _thisAni;

    private void Awake()
    {
        _thisAni = GetComponent<Animator>();
    }

    public void HitShield()
    {
        //shake all shield ui
        // _thisAni.Play("ShieldShaking");


        // StartCoroutine(ShakeAnimatorObjec());
    }

    IEnumerator ShakeAnimatorObjec()
    {
        Vector3[] pos = new Vector3[shieldAni.Length];
        for (int i = 0; i < shieldAni.Length; i++)
        {
            pos[i] = shieldAni[i].transform.position;
        }

        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(-.5f, .5f);
            float y = Random.Range(- .5f, .5f);

            for (int j = 0; j < shieldAni.Length; j++)
            {
                shieldAni[j].transform.position = new Vector3(shieldAni[j].transform.position.x + x,
                    shieldAni[j].transform.position.y + y, shieldAni[j].transform.position.z);
            }

            yield return new WaitForSeconds(0.03f);
        }

        for (int i = 0; i < shieldAni.Length; i++)
        {
            shieldAni[i].transform.position = pos[i];
        }

        yield return null;
    }

    public void BreakShield(int shieldNumber)
    {
        //play current shield break animaion 
        //disactive current shield
        if (shieldNumber < 0) return;
        shieldAni[shieldNumber].Play("ShieldBreaking");
    }

    public void ResetShield()
    {
        for (int i = 0; i < shieldAni.Length; i++)
        {
            shieldAni[i].Play("ResetShield");
        }
    }
}