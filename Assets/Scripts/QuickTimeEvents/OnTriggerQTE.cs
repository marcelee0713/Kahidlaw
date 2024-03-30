using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerQTE : MonoBehaviour
{
    [SerializeField] private QTETrigger qte;
    public UnityEvent caughtCallback;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caughtCallback.Invoke();
            qte.InitiateQTE();
        }
    }
}
