using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRefs : MonoBehaviour
{
    [Header("Chapter 2 Consequences")]
    public GameObject FidelAssistance;
    public GameObject IsabelsAloneDialogue;

    private void Start()
    {
        if(FidelAssistance  != null && IsabelsAloneDialogue != null)
        {
            Chapter2ButterflyEffect();
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
