using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDetector : MonoBehaviour
{

    public bool playerIsOnLeft = false;

    private Animator anim;
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnLeft = true;
            anim.SetFloat("Horizontal", -1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnLeft = false;
        }
    }
}
