using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private float levelTime;
    private float playerPoints;
    [SerializeField] private TextMeshProUGUI timerTextDisplay;
    [SerializeField] private TextMeshProUGUI pointsTextDisplay;

    
    // === PRIVATE METHODS === //
    
    private void Update()
    {
        UpdatePoints();
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (levelTime > 0)
        {
            levelTime -= Time.deltaTime;
        }
        else
        {
            levelTime = 0;
        }

        timerTextDisplay.text = Mathf.RoundToInt(levelTime).ToString();
    }

    private void UpdatePoints()
    {
        pointsTextDisplay.text = playerPoints.ToString();
    }

    // === PUBLIC METHODS === //
    
    public void SetTimer(float amtOfTime)
    {
        levelTime = amtOfTime;
    }

    public void AddToPoints(float amtOfPoints)
    {
        playerPoints += amtOfPoints;
    }
}
