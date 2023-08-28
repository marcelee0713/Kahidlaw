using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChanger : MonoBehaviour
{
    public static string mode;
    void Start()
    {
        mode = "Neutral";
    }

    private void Update()
    {
        Debug.Log(mode);
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
}
