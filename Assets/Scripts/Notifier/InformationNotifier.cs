using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationNotifier : MonoBehaviour
{
    public string info;
    public Environment gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.ShowNotifier(info);
        }
    }

}
