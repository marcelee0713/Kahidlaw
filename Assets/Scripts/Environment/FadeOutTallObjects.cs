using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutTallObjects : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color color;
    public float fadeOutValue = 0.80f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerFeet"))
        {
            Color fadeOut = new Color(color.r, color.g, color.b, fadeOutValue);

            sr.color = fadeOut;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFeet"))
        {
            sr.color = color;
        }
    }

}
