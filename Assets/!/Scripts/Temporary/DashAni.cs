using System.Collections;
using System.Collections.Generic;
using _.Scripts.Temporary;
using UnityEngine;

public class DashAni : MonoBehaviour
{
    public void OnBeat()
    {
        BeatManager.onBeat = true;
    }
    public void NotOnBeat()
    {
        BeatManager.onBeat = false;
    }
}