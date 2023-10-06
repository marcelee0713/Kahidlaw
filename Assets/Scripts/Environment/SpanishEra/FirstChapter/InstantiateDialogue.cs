using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDialogue : MonoBehaviour
{
    public DialogueTrigger dialogue;
    public GameObject DialogueBox;
    public DialogueManager DialogueManager;
    public float secondsToWait = 3f;

    [Header("Initalizer Face Direction")]
    public Animator anim;
    public float faceYPosition = 0f;
    public float faceXPosition = 0f;

    void Start()
    {
        DialogueManager.DisableHUD();
        if(anim != null)
        {
            anim.SetFloat("Vertical", faceYPosition);
            anim.SetFloat("Horizontal", faceXPosition);
        }
        StartCoroutine(Instantiate());
    }

    private IEnumerator Instantiate()
    {
        yield return new WaitForSeconds(secondsToWait);

        DialogueBox.SetActive(true);
        Message[] messages = dialogue.messages;
        Actor[] actors = dialogue.actors;
        DialogueManager.ChangeMessagesAndActors(messages, actors);
        dialogue.StartDialogue();
    }
}
