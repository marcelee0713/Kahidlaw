using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwoPeopleOnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent noIsabelCallback;
    [SerializeField] private UnityEvent successCallback;

    private List<int> currentPlayers = new List<int>();
    bool calledIt = false;

    private void Update()
    {
        if (currentPlayers.Count >= 2)
        {
            if (!calledIt)
            {
                calledIt = true;
                successCallback.Invoke();
            }

        } 
        else if (currentPlayers.Count == 1)
        {
            noIsabelCallback.Invoke();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Marco")
        {
            if (!currentPlayers.Contains(0))
            {
                currentPlayers.Add(0);
            }
        }

        if (collision.gameObject.name == "Isabel")
        {
            if (!currentPlayers.Contains(1))
            {
                currentPlayers.Add(1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Marco")
        {

            if (currentPlayers.Contains(0))
            {
                currentPlayers.Remove(0);
            }
        }

        if (collision.gameObject.name == "Isabel")
        {
            if (currentPlayers.Contains(1))
            {
                currentPlayers.Remove(1);
            }
        }
    }
}
