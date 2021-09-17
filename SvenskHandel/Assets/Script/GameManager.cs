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
        Time.timeScale = 1;
        Time.timeScale = 1;
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

    public float GetTimer()
    {
        return timeOnTimer;
    }
}
