using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStackingPackage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Player"))
        {
            PackageStacker.instance.StackPackage(transform);
            GetComponent<Collider>().enabled = false;
        }    
    }
}
