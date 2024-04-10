using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using UnityEngine;

public class BridSay : MonoBehaviour
{
   [SerializeField] private GameObject sayCanvas;
   private void OnTriggerStay(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         sayCanvas.SetActive(true);
      }
   }
   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         sayCanvas.SetActive(false);
      }
   }
}
