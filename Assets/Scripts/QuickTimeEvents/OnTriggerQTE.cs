using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerQTE : MonoBehaviour
{
    [SerializeField] private QTETrigger qte;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            qte.InitiateQTE();
        }
    }
}
