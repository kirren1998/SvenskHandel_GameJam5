using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum PackageDistance
{
    Short, Medium, Long
}

public enum DeliveryMethod
{
    Normal, Express, Eco
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Package : MonoBehaviour
{
    [SerializeField] private string packageID;
    [SerializeField] private float packageTimer;
    
    public PackageDistance packageDistance;
    public DeliveryMethod deliveryMethod;

    // === PUBLIC METHODS === //
    
    public void Initialize(int id, int distance, int method)
    {
        packageID = id.ToString();
        
        switch (distance)
        {
            case 1:
                packageDistance = PackageDistance.Short;
                break;
            case 2:
                packageDistance = PackageDistance.Medium;
                break;
            case 3:
                packageDistance = PackageDistance.Long;
                break;
            default:
                packageDistance = PackageDistance.Short;
                break;
        }

        switch (method)
        {
            case 1:
                deliveryMethod = DeliveryMethod.Normal;
                break;
            case 2:
                deliveryMethod = DeliveryMethod.Express;
                break;
            case 3:
                deliveryMethod = DeliveryMethod.Eco;
                break;
            default:
                deliveryMethod = DeliveryMethod.Normal;
                break;
        }

        if (deliveryMethod == DeliveryMethod.Express)
        {
            packageTimer = 60f;
        }
        else
        {
            packageTimer = 180;
        }
    }

    public void UpdateMe()
    {
        Timer();
    }

    private void Timer()
    {
        if (packageTimer > 0)
        {
            packageTimer -= Time.deltaTime;
        }
        else
        {
            packageTimer = 0;
        }
    }

    // === ACCESSORS === //

    public string GetPackageID()
    {
        return packageID;
    }
    
    public float GetPackageTimer()
    {
        return packageTimer;
    }
}
