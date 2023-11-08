using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScreen : MonoBehaviour
{
    void Update()
    {
        var camPos = Camera.main.transform;

        transform.LookAt(transform.position + camPos.rotation * Vector3.back,
            camPos.rotation * Vector3.up);
    }
}