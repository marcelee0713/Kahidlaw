using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerRefs;

public class PlayerRefs : MonoBehaviour
{
    [Header("User name")]
    public TMP_InputField inputField;
    public TextMeshProUGUI errorInputHandler;
    public GameObject nameInputPanel;
    public string currentUsername;

    [Header("Spanish Era Chapter 2 Consequences")]
    public GameObject FidelAssistance;
    public GameObject IsabelsAloneDialogue;

    [Header("Timers")]
    public Eras era;
    public float preColonialTimer;
    public float spanishEraTimer;
    public float americanEraTimer;
    public float japaneseEraTimer;
    public float martialLawEraTimer;
    public bool allowToCount = false;

    private void Start()
    {
        GetGameTimer(era);
        currentUsername = PlayerPrefs.GetString("Username", "");
        if (FidelAssistance  != null && IsabelsAloneDialogue != null)
        {
            Chapter2ButterflyEffect();
        }
    }

    private void Update()
    {
        StartGameTimer(era);
    }

    private void GetGameTimer (Eras eras)
    {
        if (!allowToCount) return;
        switch (eras)
        {
            case Eras.PreColonial:
                break;
            case Eras.Spanish:
                spanishEraTimer = PlayerPrefs.GetFloat("SpanishEraCurrentTime", 0f);
                break;
            case Eras.American:
                break;
            case Eras.Japanese:
                break;
            case Eras.MartialLaw:
                break;
        }
    }

    private void StartGameTimer(Eras eras)
    {
        if (!allowToCount) return;
        switch (eras)
        {
            case Eras.PreColonial:
                break;
            case Eras.Spanish:
                spanishEraTimer += Time.deltaTime;
                break;
            case Eras.American:
                break;
            case Eras.Japanese:
                break;
            case Eras.MartialLaw:
                break;
        }
    }

    public void SaveGameTimer (Eras eras)
    {
        switch (eras)
        {
            case Eras.PreColonial:
                break;
            case Eras.Spanish:
                float newRecord = spanishEraTimer;
                PlayerPrefs.SetFloat("SpanishEraCurrentTime", newRecord);
                break;
            case Eras.American:
                break;
            case Eras.Japanese:
                break;
            case Eras.MartialLaw:
                break;
        }
    }

    public void ClearGameTimer (Eras eras)
    {
        switch (eras)
        {
            case Eras.PreColonial:
                break;
            case Eras.Spanish:
                PlayerPrefs.SetFloat("SpanishEraCurrentTime", 0f);
                break;
            case Eras.American:
                break;
            case Eras.Japanese:
                break;
            case Eras.MartialLaw:
                break;
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

    public enum Eras
    {
        PreColonial,
        Spanish,
        American,
        Japanese,
        MartialLaw
    }
}
