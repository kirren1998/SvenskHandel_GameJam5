using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PackageFormat
{
    Small, Medium, Large
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Package : MonoBehaviour
{
    [SerializeField] private float deliveryDistance;
    public PackageFormat packageFormat;

    public void Initialize(float targetDistance, int packageSize)
    {
        deliveryDistance = targetDistance;
        
        switch (packageSize)
        {
            case 1:
                packageFormat = PackageFormat.Small;
                break;
            case 2:
                packageFormat = PackageFormat.Medium;
                break;
            case 3:
                packageFormat = PackageFormat.Large;
                break;
            default:
                packageFormat = PackageFormat.Small;
                break;
        }
    }
}
