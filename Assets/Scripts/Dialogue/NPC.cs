using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public GameObject DialogueBox;
    public Button triggerButton;
    public bool inRanged = false;

    public GameObject QuestionMark;


    private void Start()
    {
        triggerButton.interactable = inRanged;
        QuestionMark.SetActive(inRanged);
    }

    private void FixedUpdate()
    {
        triggerButton.onClick.AddListener(delegate
        {
            if (triggerButton.interactable)
            {
                DialogueBox.SetActive(true);
                dialogueTrigger.StartDialogue();

                // Also disable the button and the questionmark
                inRanged = false;
                triggerButton.interactable = inRanged;
                QuestionMark.SetActive(inRanged);
            }
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
        }
    }

}
