using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    public Image leftCharImage;
    public Image rightCharImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    public GameObject continueIcon;
    public float typingSpeed = 0.04f;


    [Header("Choices")]
    public GameObject choicesContainer;
    public Button choiceOne;
    public Button choiceTwo;
    public TextMeshProUGUI choiceOneText;
    public TextMeshProUGUI choiceTwoText;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    private Message messageToDisplay;

    [Header("Handlers")]
    public GameObject movementController;

    public static bool isActive = false;
    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = true;

    private string choiceOneRes = "";
    private string choiceTwoRes = "";

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;

        Debug.Log("Started conversation! Loaded Messages: " + messages.Length);
        isActive = true;
        DisplayMessage();
        movementController.SetActive(!isActive);
        this.gameObject.SetActive(isActive);
    }

    void DisplayMessage()
    {
        // Get the current Message Object
        messageToDisplay = currentMessages[activeMessage];

        // Displaying the Dialogue Text
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }
        displayLineCoroutine = StartCoroutine(DisplayLine(messageToDisplay.message));

        // Display the Left and Right Character
        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;

        leftCharImage.sprite = currentActors[0].sprite;
        rightCharImage.sprite = currentActors[1].sprite;

        // Display Choices
        DisplayChoices();
    }

    void DisplayChoices()
    {
        if (currentMessages[activeMessage].choices.Length != 0)
        {
            Choice choiceOne = messageToDisplay.choices[0];
            Choice choiceTwo = messageToDisplay.choices[1];

            choiceOneText.text = choiceOne.choice;
            choiceTwoText.text = choiceTwo.choice;

            choiceOneRes = choiceOne.response;
            choiceTwoRes = choiceTwo.response;
            canContinueToNextLine = false;
        } else
        {
            canContinueToNextLine = true;
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        messageText.text = "";
        continueIcon.SetActive(false);
        choicesContainer.SetActive(false);
        canContinueToNextLine = false;
        foreach (char letter in line.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if(choiceOneRes != "" && choiceTwoRes != "")
        {
            choicesContainer.SetActive(true);
        }
        else
        {
            canContinueToNextLine = true;
        }

        continueIcon.SetActive(true);

    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation ended!");
            isActive = false;
            this.gameObject.SetActive(isActive);
            movementController.SetActive(!isActive);
        }
    }

    void Start()
    {
        this.gameObject.SetActive(isActive);

        // If choice one is pressed
        choiceOne.onClick.AddListener(delegate
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string choiceHolder = choiceOneRes;

            ResetAndDeactivateChoices();
            displayLineCoroutine = StartCoroutine(DisplayLine(choiceHolder));

            

        });

        // If choice two is pressed
        choiceTwo.onClick.AddListener(delegate
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string choiceHolder = choiceTwoRes;

            ResetAndDeactivateChoices();
            displayLineCoroutine = StartCoroutine(DisplayLine(choiceHolder));
        });
    }

    void ResetAndDeactivateChoices()
    {
        // Clear out the response text
        choiceOneRes = "";
        choiceTwoRes = "";

        // Clear out the button's text
        choiceOneText.text = "";
        choiceTwoText.text = "";

        // Close down the Choices Container
        choicesContainer.SetActive(false);

        // Can now continue to the next line
        canContinueToNextLine = true;

    }

    void Update()
    {

    }

    public void HandleNextMessage()
    {
        if(isActive && canContinueToNextLine)
        {
            NextMessage();
        }
    }
}