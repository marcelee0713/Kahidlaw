using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollideCallback : MonoBehaviour
{
    [SerializeField] private UnityEvent onCollisionCallback;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onCollisionCallback.Invoke();
        }
    }
}
