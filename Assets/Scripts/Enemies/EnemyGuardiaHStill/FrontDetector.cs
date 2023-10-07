using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDetector : MonoBehaviour
{
    public bool playerIsOnFront = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnFront = true;
            anim.SetFloat("Vertical", -1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnFront = false;
        }
    }
}
