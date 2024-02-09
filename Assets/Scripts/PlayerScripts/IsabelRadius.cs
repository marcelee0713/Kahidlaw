using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsabelRadius : MonoBehaviour
{
    [SerializeField] private PlayerMovement isabelMovement;
    [SerializeField] private PlayerMovement marcoMovement;
    [SerializeField] private bool onRadius = true;
    [SerializeField] private bool turnOn = true;

    [Header("Dialogues")]
    public DialogueTrigger dialogue;
    public GameObject DialogueBox;
    public DialogueManager DialogueManager;

    private void Update()
    {
        if(turnOn)
        {
            marcoMovement.AllowToMoveBoth = onRadius;
            isabelMovement.AllowToMoveBoth = onRadius;
            return;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Isabel")
        {
            onRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Isabel")
        {
            onRadius = false;
            if (turnOn)
            {
                DialogueBox.SetActive(true);
                Message[] messages = dialogue.messages;
                Actor[] actors = dialogue.actors;
                DialogueManager.ChangeMessagesAndActors(messages, actors);
                dialogue.StartDialogue();
            }

        }
    }
}
