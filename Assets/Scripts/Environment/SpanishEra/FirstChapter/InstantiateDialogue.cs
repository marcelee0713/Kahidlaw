using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDialogue : MonoBehaviour
{
    public Animator anim;
    public DialogueTrigger dialogue;
    public GameObject DialogueBox;
    public DialogueManager DialogueManager;

    void Start()
    {
        DialogueManager.DisableHUD();
        anim.SetFloat("Vertical", -4f);
        StartCoroutine(Instantiate());
    }

    void Update()
    {
        
    }

    private IEnumerator Instantiate()
    {
        yield return new WaitForSeconds(3f);

        DialogueBox.SetActive(true);
        dialogue.StartDialogue();
    }
}
