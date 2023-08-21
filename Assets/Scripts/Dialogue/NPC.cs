using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public GameObject DialogueBox;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DialogueBox.SetActive(true);
            dialogueTrigger.StartDialogue();
        }
    }
}
