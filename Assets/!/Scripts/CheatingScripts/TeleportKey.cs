using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportKey : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject key;
    [SerializeField] private Transform teleportLocation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            player.transform.position = teleportLocation.position + new Vector3(5, 0, 0);
            key.transform.position = teleportLocation.position;
        }
    }
}