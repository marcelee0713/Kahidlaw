using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TwoDoorsTwoPeople : MonoBehaviour
{
    [SerializeField] private Button interact;
    [SerializeField] UnityEvent callback;
    private UnityAction action;

    private List<int> currentPlayers = new List<int>();

    private void Start()
    {
        action = () =>
        {
            if (interact.interactable)
            {
                callback.Invoke();
            }
        };
    }

    private void Update()
    {
        if(currentPlayers.Count >= 2)
        {
            interact.interactable = true;
            interact.onClick.RemoveListener(action);
            interact.onClick.AddListener(action);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Marco")
        {
            if(!currentPlayers.Contains(0))
            {
                currentPlayers.Add(0);
            }
        }

        if (collision.gameObject.name == "Isabel")
        {
            if(!currentPlayers.Contains(1))
            {
                currentPlayers.Add(1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Marco")
        {
            interact.interactable = false;
            interact.onClick.RemoveListener(action);

            if(currentPlayers.Contains(0))
            {
                currentPlayers.Remove(0);
            }
        }

        if (collision.gameObject.name == "Isabel")
        {
            interact.interactable = false;
            interact.onClick.RemoveListener(action);

            if (currentPlayers.Contains(1))
            {
                currentPlayers.Remove(1);
            }
        }
    }
}
