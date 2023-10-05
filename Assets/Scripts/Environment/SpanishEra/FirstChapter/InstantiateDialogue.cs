using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDialogue : MonoBehaviour
{
    public Animator anim;
    public DialogueTrigger dialogue;
    public GameObject DialogueBox;
    public DialogueManager DialogueManager;
    public float secondsToWait = 3f;

    void Start()
    {
        DialogueManager.DisableHUD();
        if(anim != null)
        {
            anim.SetFloat("Vertical", -4f);
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
