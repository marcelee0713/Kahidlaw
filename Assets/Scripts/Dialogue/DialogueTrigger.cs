using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string talkingToWho;
    public Message[] messages;
    public Actor[] actors;
    public Environment gameManager;

    public void StartDialogue()
    {
        gameManager.UpdateMission(0);
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public string choice;
    public string response;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
