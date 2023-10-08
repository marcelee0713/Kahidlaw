using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChanger : MonoBehaviour
{
    public static string mode;

    public Camera MarcoCamera;
    public Camera IsabelCamera;
    public string character = "";
    public string changeMode = "";
    public static string currentCharacter = "Marco";



    void Start()
    {
        if(changeMode != "")
        {
            mode = changeMode;
        } 
        else
        {
            mode = "Neutral";
        }
        currentCharacter = character;
    }

    private void Update()
    {
        if (currentCharacter == "Marco")
        {

            CheckAndRun(IsabelCamera, () => IsabelCamera.enabled = false); 
            MarcoCamera.enabled = true;

        } else
        {
            CheckAndRun(MarcoCamera, () => MarcoCamera.enabled = false);
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

    public void SetMode(string changeMode)
    {
        mode = changeMode;
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

    public delegate void Callback();

    public void CheckAndRun(Camera thisGameObject, Callback callback)
    {
        if (thisGameObject != null)
        {
            callback();
        }
    }
}
