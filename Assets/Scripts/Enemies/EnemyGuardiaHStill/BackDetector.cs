using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDetector : MonoBehaviour
{
    public bool playerIsOnBack = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnBack = true;
            anim.SetFloat("Vertical", 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnBack = false;
        }
    }
}
