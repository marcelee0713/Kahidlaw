using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CTF : MonoBehaviour
{
    // check out the excalidraw for the logic. 
    // Also make the npcs or allies go to the flag
    [Header("UI & Instances")]
    [SerializeField] private GameObject barObject;
    [SerializeField] private Slider bar;
    [SerializeField] private float value;
    [SerializeField] private float maxValue;

    private bool isCaptured = false;
    [Header("Current Majority")]
    public bool playerMajority = false;
    public bool enemyMajority = false;

    [Header("Flag Radius Related")]
    [SerializeField] private SpriteRenderer radiusSr;
    [SerializeField] private Color playerColor;
    [SerializeField] private Color enemyColor;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
