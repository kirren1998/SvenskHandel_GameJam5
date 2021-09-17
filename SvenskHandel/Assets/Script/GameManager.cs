using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private float timeOnTimer;
    [SerializeField] private HUDManager hudManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (hudManager != null)
        {
            hudManager.SetTimer(timeOnTimer);
        }
        else
        {
            Debug.LogError("You've not assigned the HUDManager to the GameManager object Script. Please assign it before playing.");
        }
    }

    public void GivePoints(float amtOfPoints)
    {
        hudManager.AddToPoints(amtOfPoints);
    }

    public void ErasePoints(float amtOfPoints)
    {
        hudManager.AddToPoints(amtOfPoints);
    }
}
