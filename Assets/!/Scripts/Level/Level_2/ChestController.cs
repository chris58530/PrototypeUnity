using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpened = false;

    public void OpenChest()
    {
        if (!isOpened)
        {
            isOpened = true;
        }
    }

    public void ChestBite()
    {
    }
}