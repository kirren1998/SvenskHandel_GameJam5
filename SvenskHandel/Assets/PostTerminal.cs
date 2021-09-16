using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PostTerminal : MonoBehaviour
{
   private float m_Dd;
   [SerializeField] private TerminalPf tpf;

   enum TerminalPf
   {
      Plane, Boat, Truck, Train
   }
   public void DeliverPackage()
   {
      switch (tpf)
      {
         case TerminalPf.Plane:
            if (m_Dd<10000)
            {
               //
            }
            break;
         case TerminalPf.Boat:
            break;
         case TerminalPf.Train:
            break;
         case TerminalPf.Truck:
            break;
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Pickup"))
      {
         m_Dd= other.GetComponent<Package>().GetDeliveryDistance();
      }
   }
}
