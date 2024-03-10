using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Deathmatch : MonoBehaviour
{
    public Teams currentTeam;
    public Slider marcoSlider;
    public Slider isabelSlider;

    public GameObject[] isabelsAllies;
    public GameObject[] marcosAllies;

    public GameObject enemySlainedNotifier;

    public UnityEvent isabelWinsCallback;
    public UnityEvent marcoWinsCallback;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public enum Teams
    {
        ISABEL,
        MARCO,
    }
}
