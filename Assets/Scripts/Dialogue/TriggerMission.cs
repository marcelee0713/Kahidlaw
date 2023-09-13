using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TriggerMission : MonoBehaviour
{
    public UnityEngine.UI.Button triggerButton;
    public bool inRanged = false;
    public GameObject QuestionMark;

    public UnityEvent eventCallback;
    private UnityAction action;


    private void Awake()
    {
        triggerButton.interactable = inRanged;
        QuestionMark.SetActive(inRanged);

        action = () =>
        {
            Debug.Log("Am I clicked?");
            if (triggerButton.interactable)
            {
                eventCallback.Invoke();
                inRanged = false;
                triggerButton.interactable = inRanged;
                QuestionMark.SetActive(inRanged);
                triggerButton.onClick.RemoveListener(action);
            }
        };


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerButton.onClick.RemoveListener(action);

            triggerButton.onClick.AddListener(action);

            inRanged = true;
            triggerButton.interactable = inRanged;
            QuestionMark.SetActive(inRanged);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRanged = false;
            triggerButton.interactable = inRanged;
            QuestionMark.SetActive(inRanged);
            triggerButton.onClick.RemoveListener(action);

        }
    }
}
