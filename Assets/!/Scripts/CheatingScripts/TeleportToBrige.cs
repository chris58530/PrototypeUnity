using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToBrige : MonoBehaviour
{
    [SerializeField] private Transform teleportLocation;
    [SerializeField] private GameObject player;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            player.transform.position = teleportLocation.position;

        }
    }
}
