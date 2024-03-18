using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
   [SerializeField] private GameObject openFire;

   public void OpenTorchLight()
   {
      openFire.SetActive(true);  
   }
   
   public void CloseTorchLight()
   {
      openFire.SetActive(false);  
   }

 
}
