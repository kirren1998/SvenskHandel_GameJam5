using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermialInfo : MonoBehaviour
{
    [Header("Base Info")]
    [SerializeField] private Image[] slots;
    [SerializeField] [Range(1, 10)] private int maxSlots;

    [Header("Info(just for show)")]
    [SerializeField] private int slotsFilled;

}
