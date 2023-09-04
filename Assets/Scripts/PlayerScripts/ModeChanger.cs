using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChanger : MonoBehaviour
{
    public static string mode;
    public static string currentCharacter = "Marco";

    public Camera MarcoCamera;
    public Camera IsabelCamera;


    void Start()
    {
        mode = "Neutral";
    }

    private void Update()
    {
        if (currentCharacter == "Marco")
        {
            MarcoCamera.enabled = true;
            IsabelCamera.enabled = false;
        } else
        {
            MarcoCamera.enabled = false;
            IsabelCamera.enabled = true;
        }
    }

    public void HandleChangeMode()
    {
        if (mode == "Neutral")
        {
            mode = "Gun";
        }
        else if (mode == "Gun")
        {
            mode = "Melee";
        }
        else
        {
            mode = "Neutral";
        }
    }

    public void HandleChangeCharacter()
    {
        if(currentCharacter == "Marco")
        {
            currentCharacter = "Isabel";
        }
        else if (currentCharacter == "Isabel")
        {
            currentCharacter = "Marco";
        }
    }
}
