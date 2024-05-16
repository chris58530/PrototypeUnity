using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToBell : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform teleportLocation;
    [SerializeField] private GameObject[] destroyObjects;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            foreach (var obj in destroyObjects)
            {
                Destroy(obj);
            }

            player.transform.position = teleportLocation.position;
            FindObjectOfType<Bell>().canPlay = true;
        }
    }
}