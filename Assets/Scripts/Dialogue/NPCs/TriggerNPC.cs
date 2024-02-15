using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerNPC : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private DialogueManager dialogueManager;

    public GameObject DialogueBox;

    [SerializeField] private Animator npcAnim;
    [SerializeField] private float yDirection;
    [SerializeField] private float xDirection;

    public UnityEvent callback;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            callback.Invoke();

            if(npcAnim != null)
            {
                npcAnim.SetFloat("Vertical", yDirection);
                npcAnim.SetFloat("Horizontal", xDirection);
            }

            Message[] newMessages = dialogueTrigger.messages;
            Actor[] newActors = dialogueTrigger.actors;
            UnityEvent unityEvent = dialogueTrigger.StartCallback;

            DialogueManager.currentMessages = newMessages;
            DialogueManager.currentActors = newActors;

            DialogueBox.SetActive(true);
            dialogueTrigger.StartDialogue();
            unityEvent.Invoke();
        }
    }
}
