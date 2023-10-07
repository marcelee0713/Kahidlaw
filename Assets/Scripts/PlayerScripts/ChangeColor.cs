using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color newColor;
    private SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.color = newColor;
    }

    public void HurtColor()
    {
        rend.material.color = new Color(255f, 0, 0);
    }
}
