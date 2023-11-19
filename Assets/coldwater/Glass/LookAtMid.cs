using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMid : MonoBehaviour
{
    public GameObject laserPoint;

    void Update()
    {
        transform.LookAt(laserPoint.transform.position);
    }
}
