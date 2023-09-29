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
    public Sprite messageTwoSprite;
    public UnityEvent messageCallback;
}

[System.Serializable]
public class Choice
{
    public string choice;
    public string response;
    public UnityEvent callback;
    public Sprite actor1Reaction;
    public Sprite actor2Reaction;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
