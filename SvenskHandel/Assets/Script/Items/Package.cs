using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
    private float maxTime;
    
    public PackageDistance packageDistance;
    public DeliveryMethod deliveryMethod;
    
    private Image myTimerImage;

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

        maxTime = packageTimer;
    }

    public void UpdateMe()
    {
        Timer();
    }

    private void Timer()
    {
        if (packageTimer > 0)
        {
            float percentTimeLeft = packageTimer / maxTime;
            
            packageTimer -= Time.deltaTime;
            GetComponent<MeshRenderer>().material.color = new Color(1 - percentTimeLeft, percentTimeLeft, 0);
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
