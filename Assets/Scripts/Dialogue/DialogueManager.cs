using System;
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

    public static Message[] currentMessages;
    public static Actor[] currentActors;
    int activeMessage = 0;
    private Message messageToDisplay;

    [Header("Handlers")]
    public GameObject movementController;
    public GameObject gunController;
    public GameObject talkButton;
    public GameObject switchButton;
    public GameObject healthBar;
    public GameObject missionsBar;
    public GameObject missionsPanel;
    public GameObject locatorBar;
    public GameObject notifierBar;
    public GameObject meleeController;
    public GameObject switchCharButton;
    public GameObject settingsButton;
    public GameObject hinter;

    public static bool isDialogueActive = false;
    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = true;

    private string choiceOneRes = "";
    private string choiceTwoRes = "";

    private bool hasSkippedDialogue = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && messageToDisplay != null)
        {
            hasSkippedDialogue = true;
        }
    }

    public void OpenDialogue()
    {
        activeMessage = 0;

        isDialogueActive = true;
        DisableHUD();
        DisplayMessage();
        this.gameObject.SetActive(isDialogueActive);
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

        // Display the Left and Right Character's Name
        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;

        if (currentActors.Length == 2)
        {
            rightCharImage.enabled = true;
            leftCharImage.sprite = currentActors[0].sprite;
            rightCharImage.sprite = currentActors[1].sprite;
        }
        else
        {
            if(currentActors[0].sprite == null)
            {
                leftCharImage.enabled = false;
            }
            else
            {
                leftCharImage.sprite = currentActors[0].sprite;
            }
            rightCharImage.enabled = false;
        }

        // Display Characters reaction if they have one
        if (currentMessages[activeMessage].messageSprite != null)
        {
            leftCharImage.sprite = currentMessages[activeMessage].messageSprite;
        }

        if (currentMessages[activeMessage].messageTwoSprite != null)
        {
            rightCharImage.sprite = currentMessages[activeMessage].messageTwoSprite;
        }
        
        // Flipping Images
        if (currentActors[0].flipImage)
        {
            leftCharImage.rectTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        
        if (currentActors[1].flipImage)
        {
            rightCharImage.rectTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

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
        }
        else
        {
            canContinueToNextLine = true;
        }
    }

    // In order to have the typing effect
    // Also checks if some "choices" exist
    private IEnumerator DisplayLine(string line)
    {
        hasSkippedDialogue = false;
        messageText.text = "";
        continueIcon.SetActive(false);
        choicesContainer.SetActive(false);
        canContinueToNextLine = false;
        foreach (char letter in line.ToCharArray())
        {
            if (hasSkippedDialogue)
            {
                messageText.text = line;
                break;
            }

            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (choiceOneRes != "" && choiceTwoRes != "")
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

        // Invoke or Call the Message Callback
        if(currentMessages[activeMessage].messageCallback != null)
        {
            currentMessages[activeMessage].messageCallback.Invoke();
        }

        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            leftCharImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rightCharImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            bool disabledHUD = currentMessages[activeMessage - 1].disableHUDAfterDialogue;
            
            isDialogueActive = false;
            this.gameObject.SetActive(isDialogueActive);
            if (!disabledHUD)
            {
                EnableHUD();
            }
        }
    }

    void Start()
    {
        this.gameObject.SetActive(isDialogueActive);

        // If choice one is pressed
        choiceOne.onClick.AddListener(delegate
        {
            // Check if there are any functions or callback this choice has
            if (messageToDisplay.choices[0] != null)
            {
                Choice choiceOne = messageToDisplay.choices[0];
                choiceOne.callback.Invoke();

            }

            // Check if there are any reactions of this choice
            if (messageToDisplay.choices[0].actor1Reaction != null)
            {
                leftCharImage.sprite = messageToDisplay.choices[0].actor1Reaction;
            }

            if (messageToDisplay.choices[0].actor2Reaction != null)
            {
                rightCharImage.sprite = messageToDisplay.choices[0].actor2Reaction;
            }

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
            // Check if there are any functions or callback this choice has
            if (messageToDisplay.choices[1] != null)
            {
                Choice choiceTwo = messageToDisplay.choices[1];
                choiceTwo.callback.Invoke();

            }

            // Check if there are any reactions of this choice
            if (messageToDisplay.choices[0].actor1Reaction != null)
            {
                leftCharImage.sprite = messageToDisplay.choices[0].actor1Reaction;
            }

            if (messageToDisplay.choices[0].actor2Reaction != null)
            {
                rightCharImage.sprite = messageToDisplay.choices[0].actor2Reaction;
            }

            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string choiceHolder = choiceTwoRes;

            ResetAndDeactivateChoices();
            displayLineCoroutine = StartCoroutine(DisplayLine(choiceHolder));
        });
    }

    public void ChangeMessagesAndActors(Message[] newMessages, Actor[] newActors)
    {
        currentMessages = newMessages;
        currentActors = newActors;
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

    public void HandleNextMessage()
    {
        if (isDialogueActive && canContinueToNextLine)
        {
            NextMessage();
        }
    }

    public void SetSwitchButton(bool setSwitch)
    {
        CheckAndRun(switchButton, () => switchButton.SetActive(setSwitch));

    }

    public void ChangeFirstActorName(string newFirstActorName)
    {
        currentActors[0].name = newFirstActorName;
    }

    public void ChangeSecondActorName(string newSecondActorName)
    {
        currentActors[1].name = newSecondActorName;
    }

    public void DisableHUD()
    {
        CheckAndRun(movementController, () => movementController.SetActive(false));
        CheckAndRun(gunController, () => gunController.SetActive(false));
        CheckAndRun(talkButton, () => talkButton.SetActive(false));
        CheckAndRun(switchButton, () => switchButton.SetActive(false));
        CheckAndRun(healthBar, () => healthBar.SetActive(false));
        CheckAndRun(missionsBar, () => missionsBar.SetActive(false));
        CheckAndRun(missionsPanel, () => missionsPanel.SetActive(false));
        CheckAndRun(locatorBar, () => locatorBar.SetActive(false));
        CheckAndRun(notifierBar, () => notifierBar.SetActive(false));
        CheckAndRun(meleeController, () => meleeController.SetActive(false));
        CheckAndRun(switchCharButton, () => switchCharButton.SetActive(false));
        CheckAndRun(hinter, () => hinter.SetActive(false));
    }

    public void DisableSettingsButton()
    {
        ModeChanger.mode = "Neutral";
        settingsButton.SetActive(false);
    }

    void EnableHUD()
    {
        CheckAndRun(movementController, () => movementController.SetActive(true));


        if (ModeChanger.mode == "Gun")
        {
            CheckAndRun(gunController, () => gunController.SetActive(true));

        }
        else if (ModeChanger.mode == "Melee")
        {
            CheckAndRun(meleeController, () => meleeController.SetActive(true));
        }

        if (Environment.isMissionPanelOpen)
        {
            CheckAndRun(missionsPanel, () => missionsPanel.SetActive(true));
        }

        /*
        talkButton.SetActive(true);
        switchButton.SetActive(true);
        healthBar.SetActive(true);
        missionsBar.SetActive(true);
        locatorBar.SetActive(true);
        switchCharButton.SetActive(true);
        */

        CheckAndRun(talkButton, () => talkButton.SetActive(true));
        CheckAndRun(switchButton, () => switchButton.SetActive(true));
        CheckAndRun(healthBar, () => healthBar.SetActive(true));
        CheckAndRun(missionsBar, () => missionsBar.SetActive(true));
        CheckAndRun(locatorBar, () => locatorBar.SetActive(true));
        CheckAndRun(switchCharButton, () => switchCharButton.SetActive(true));
        CheckAndRun(hinter, () => hinter.SetActive(true));

    }

    public delegate void Callback();

    public void CheckAndRun(GameObject thisGameObject,Callback callback)
    {
        if(thisGameObject != null)
        {
            callback();
        }
    }
}
