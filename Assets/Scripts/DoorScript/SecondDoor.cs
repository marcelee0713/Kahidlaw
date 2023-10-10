using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SecondDoor : MonoBehaviour
{
    [SerializeField] private Button interact;
    private UnityAction action;
    private bool inRanged = false;
    private DoorToDoor doorManager;

    public UnityEvent onDoorCallback;

    private void Start()
    {
        doorManager = GetComponentInParent<DoorToDoor>();

        action = () =>
        {
            if (interact.interactable)
            {
                onDoorCallback.Invoke();
                doorManager.GoToFirstDoor();
            }
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            interact.onClick.RemoveListener(action);
            interact.onClick.AddListener(action);

            inRanged = true;
            interact.interactable = inRanged;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRanged = false;
            interact.interactable = inRanged;
            interact.onClick.RemoveListener(action);
        }
    }
}
