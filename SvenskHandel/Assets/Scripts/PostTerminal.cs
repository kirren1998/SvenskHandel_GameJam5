using System;
using System.Collections;
using System.Collections.Generic;
using Script.Player;
using UnityEngine;

public class PostTerminal : MonoBehaviour
{
   private float m_Dd;
   [SerializeField] private TerminalPf tpf;
   private Package m_Package;
   
   //The terminal sorts 
   enum TerminalPf
   {
      Plane, Boat, Truck, Train
   }
   /// <summary>
   /// Checks if the package is the right kind for the terminal
   /// </summary>
   public void DeliverPackage()
   {
      switch (tpf)
      {
         case TerminalPf.Plane:
         {
            if (m_Package.deliveryMethod != DeliveryMethod.Eco || m_Package.packageDistance != PackageDistance.Short)
            {
               //Give Points
            }
            else
            {
               //Delete Points
            }
         }
            break;
         case TerminalPf.Boat:
            if (m_Package.deliveryMethod != DeliveryMethod.Express || m_Package.packageDistance != PackageDistance.Short)
            {
               //Give Points
            }
            else
            {
               //Delete Points
            }
            break;
         case TerminalPf.Train:
            if (m_Package.deliveryMethod != DeliveryMethod.Express || m_Package.packageDistance != PackageDistance.Long)
            {
               //Give Points
            }
            else
            {
               //Delete Points
            }
            break;
         case TerminalPf.Truck:
            if (m_Package.deliveryMethod != DeliveryMethod.Eco || m_Package.packageDistance != PackageDistance.Short)
            {
               //Give Points
            }
            else
            {
               //Delete Points
            }
            break;
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Pickup"))
      {
         m_Package= other.GetComponent<Package>();
         Destroy(other.gameObject);
      }
   }
}
