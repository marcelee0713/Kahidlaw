using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Data")]
    public Message[] messages;
    public Actor[] actors;

    [Header("Management and Callback")]
    public Environment gameManager;
    public UnityEvent StartCallback;

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue();
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
    public Choice[] choices;
    public Sprite messageSprite;
}

[System.Serializable]
public class Choice
{
    public string choice;
    public string response;
    public UnityEvent callback;
    public Sprite spriteReaction;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
