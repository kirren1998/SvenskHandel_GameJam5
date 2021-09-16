using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostTerminal : MonoBehaviour
{
   
   private void Start()
   {
      throw new NotImplementedException();
   }

   enum DeliveryFormat
   {
      Small, Medium, Large
   }
   
   public void DeliverPackage()
   {
      if()
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Pickup"))
      {
         
      }
   }
}
