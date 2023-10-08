using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VerticalStealthBehavior : MonoBehaviour
{
    [Header("Vertical Stealth Properties")]
    public float distance;
    RaycastHit2D hit;

    private NPCPatrolVertical patrol;

    [Header("Dialogue Caught")]
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private DialogueManager dialogueManager;

    public GameObject originDetector;

    public GameObject DialogueBox;

    void Start()
    {
        patrol = GetComponentInParent<NPCPatrolVertical>();
        patrol.enabled = true;
    }

    void FixedUpdate()
    {
        float chracterDirection = patrol.movingDown ? -1f : 1f;

        hit = Physics2D.Raycast(originDetector.transform.position, new Vector2(0f, chracterDirection), distance);

        if (hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                if (!DialogueManager.isDialogueActive && !HealthSystem.playerIsDead)
                {
                    Message[] newMessages = dialogueTrigger.messages;
                    Actor[] newActors = dialogueTrigger.actors;
                    UnityEvent unityEvent = dialogueTrigger.StartCallback;

                    unityEvent.Invoke();

                    DialogueManager.currentMessages = newMessages;
                    DialogueManager.currentActors = newActors;

                    DialogueBox.SetActive(true);
                    dialogueTrigger.StartDialogue();
                }

                Debug.Log("Did hit player");
                Debug.DrawRay(originDetector.transform.position, hit.distance * new Vector2(0f, chracterDirection), Color.red);
                patrol.enabled = false;
            }

            else
            {
                Debug.DrawRay(originDetector.transform.position, distance * new Vector2(0f, chracterDirection), Color.green);
                patrol.enabled = true;
            }

        }

    }
}
