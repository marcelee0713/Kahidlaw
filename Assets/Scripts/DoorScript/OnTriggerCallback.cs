using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerCallback : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggerCallBack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OnTriggerCallBack.Invoke();
        }
    }
}
