using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCanvas : MonoBehaviour
{
    [SerializeField]private GameObject[] values;
 

    public void SetAbilityQuanity(int count)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i].SetActive(false);
        }

        switch (count)
        {
            case 0:
                return;
            case 1:
                values[0].SetActive(true);
                break;
            case 2:
                values[0].SetActive(true);
                values[1].SetActive(true);
                break;
            case 3:
                values[0].SetActive(true);
                values[1].SetActive(true);
                values[2].SetActive(true);
                break;
            case 4:
                values[0].SetActive(true);
                values[1].SetActive(true);
                values[2].SetActive(true);
                values[3].SetActive(true);
                break;
        }
    }
}