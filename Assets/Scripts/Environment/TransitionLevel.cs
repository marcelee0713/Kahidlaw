using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TransitionLevel : MonoBehaviour
{
    [Header("Essentials")]
    public Environment gameManager;
    public Button interactButton;
    private bool inRanged = false;
    private UnityAction action;
    public UnityEvent GoToScene;

    public string location;

    private void Awake()
    {
        interactButton.interactable = inRanged;

        action = () =>
        {
            if (interactButton.interactable)
            {
                GoToScene.Invoke();
                interactButton.onClick.RemoveListener(action);
            }
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactButton.onClick.RemoveListener(action);
            interactButton.onClick.AddListener(action);

            inRanged = true;
            interactButton.interactable = inRanged;
            gameManager.ShowNotifier(location);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRanged = false;
            interactButton.interactable = inRanged;
            interactButton.onClick.RemoveListener(action);
        }
    }
}
