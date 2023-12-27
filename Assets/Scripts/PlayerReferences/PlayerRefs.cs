using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRefs : MonoBehaviour
{
    [Header("User name")]
    public TMP_InputField inputField;
    public TextMeshProUGUI errorInputHandler;
    public GameObject nameInputPanel;

    [Header("Chapter 2 Consequences")]
    public GameObject FidelAssistance;
    public GameObject IsabelsAloneDialogue;

    private void Awake()
    {
        inputField = GameObject.Find("NameField").GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        if (FidelAssistance  != null && IsabelsAloneDialogue != null)
        {
            Chapter2ButterflyEffect();
        }
    }

    public void StoreName()
    {
        string userName = inputField.text;

        if (userName == "")
        {
            errorInputHandler.text = "Enter your name.";
        }
        else if (userName.Length < 2 )
        {
            errorInputHandler.text = "Name is too short";
        }
        else if (userName.Length >= 15)
        {
            errorInputHandler.text = "Name is too long.";
        } 
        else
        {
            errorInputHandler.text = "";
            nameInputPanel.SetActive(false);
            PlayerPrefs.SetString("Username", userName);
        }
    }

    public string GetHasBeenAssaulted()
    {
        string hasBeenAssaulted = PlayerPrefs.GetString("Chapter2-Assaulted", "y");
        return hasBeenAssaulted;
    }

    public string DidHelpedTheIndios()
    {
        string isabelHelped = PlayerPrefs.GetString("Chapter2-HelpedIndios", "n");
        return isabelHelped;
    }

    public void SetHelpedTheIndios(string choice)
    {
        PlayerPrefs.SetString("Chapter2-HelpedIndios", choice);
    }

    public void SetHasBeenAssaulted(string choice)
    {
        PlayerPrefs.SetString("Chapter2-Assaulted", choice);
    }

    public void SpanishEraSaveGame(string savename)
    {
        if(savename != "")
        {
            PlayerPrefs.SetString("Spanish-Era", savename);
        }
    }

    private void Chapter2ButterflyEffect()
    {
        if (DidHelpedTheIndios() == "y")
        {
            FidelAssistance.SetActive(true);
            IsabelsAloneDialogue.SetActive(false);
        }
        else if (DidHelpedTheIndios() == "n")
        {
            FidelAssistance.SetActive(false);
            IsabelsAloneDialogue.SetActive(true);
        }
    }
}
