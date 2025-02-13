using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerRefs;

public class PlayerRefs : MonoBehaviour
{
    [Header("User name")]
    public TMP_InputField inputField;
    public TextMeshProUGUI errorInputHandler;
    public GameObject nameInputPanel;
    public string currentUsername;

    [Header("Pre Colonial Era Consequences")]
    public GameObject LapuLapu;

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

    [Header("SFX & Background Music")]
    public AudioSource[] SFXs;
    public AudioSource[] BackgroundMusics;
    public TextMeshProUGUI sfxDash;
    public TextMeshProUGUI backgroundMusicDash;


    private void Awake()
    {
        InstantiateSounds();
    }

    private void Start()
    {
        InstantiateGameTimer(era);

        currentUsername = PlayerPrefs.GetString("Username", "");

        if (currentUsername == "")
        {
            CheckAndRunThisGameObject(nameInputPanel, () => nameInputPanel.SetActive(true));
        }

        if (FidelAssistance != null && IsabelsAloneDialogue != null)
        {
            Chapter2ButterflyEffect();
        }

        IsWithLapuLapu();
    }

    private void Update()
    {
        StartGameTimer(era);
    }

    // Sound Effects & Music Prefs

    public void InstantiateSounds()
    {
        string BgMusicMuted = PlayerPrefs.GetString("BackgroundMusic", "false");
        string sfxMuted = PlayerPrefs.GetString("SFX", "false");

        if (BgMusicMuted == "true")
        {
            CheckAndRunText(backgroundMusicDash, () => backgroundMusicDash.text = "/");
            foreach (AudioSource audio in BackgroundMusics)
            {
                audio.volume = 0.0f;
            }
        }
        else 
        {
            CheckAndRunText(backgroundMusicDash, () => backgroundMusicDash.text = "");
            foreach (AudioSource audio in BackgroundMusics)
            {
                audio.volume = 0.5f;
            }
        }

        if (sfxMuted == "true")
        {
            CheckAndRunText(sfxDash, () => sfxDash.text = "/");
            foreach (AudioSource sfx in SFXs)
            {
                sfx.volume = 0.0f;
            }

        }
        else
        {
            CheckAndRunText(sfxDash, () => sfxDash.text = "");
            foreach (AudioSource sfx in SFXs)
            {
                sfx.volume = 0.45f;
            }
        }
    }

    public void ToggleBgMusic()
    {
        string BgMusicMuted = PlayerPrefs.GetString("BackgroundMusic", "false");

        if (BgMusicMuted == "true")
        {
            foreach (AudioSource audio in BackgroundMusics)
            {
                audio.volume = 0.5f;
            }
            CheckAndRunText(backgroundMusicDash, () => backgroundMusicDash.text = ""); 
            PlayerPrefs.SetString("BackgroundMusic", "false");
        }
        else
        {
            foreach (AudioSource audio in BackgroundMusics)
            {
                audio.volume = 0.0f;
            }
            CheckAndRunText(backgroundMusicDash, () => backgroundMusicDash.text = "/");
            PlayerPrefs.SetString("BackgroundMusic", "true");
        }
    }

    public void ToggleSFXMusic()
    {
        string sfxMuted = PlayerPrefs.GetString("SFX", "false");

        if (sfxMuted == "true")
        {
            foreach (AudioSource sfx in SFXs)
            {
                sfx.volume = 0.45f;
            }
            CheckAndRunText(sfxDash, () => sfxDash.text = "");
            PlayerPrefs.SetString("SFX", "false");
        }
        else
        {
            foreach (AudioSource sfx in SFXs)
            {
                sfx.volume = 0.0f;
            }
            CheckAndRunText(sfxDash, () => sfxDash.text = "/");
            PlayerPrefs.SetString("SFX", "true");
        }
    }

    // Timer Refs
    public void PauseGameTimer()
    {
        allowToCount = false;
    }

    public void ResumeGameTimer() {
    
        allowToCount = true;
    }

    private void InstantiateGameTimer(Eras eras)
    {
        if (!allowToCount) return;
        switch (eras)
        {
            case Eras.PreColonial:
                preColonialTimer = PlayerPrefs.GetFloat("PreColonialEraCurrentTime", 0f);
                break;
            case Eras.Spanish:
                spanishEraTimer = PlayerPrefs.GetFloat("SpanishEraCurrentTime", 0f);
                break;
            case Eras.American:
                americanEraTimer = PlayerPrefs.GetFloat("AmericanEraCurrentTime", 0f);
                break;
            case Eras.Japanese:
                japaneseEraTimer = PlayerPrefs.GetFloat("JapaneseEraCurrentTime", 0f);
                break;
            case Eras.MartialLaw:
                martialLawEraTimer = PlayerPrefs.GetFloat("MartialLawEraCurrentTime", 0f);
                break;
        }
    }

    public float GetGameTImer(Eras eras)
    {
        switch (eras)
        {
            case Eras.PreColonial:
                float timer_1= PlayerPrefs.GetFloat("PreColonialEraCurrentTime", 0f);
                return timer_1;
            case Eras.Spanish:
                float timer_2 = PlayerPrefs.GetFloat("SpanishEraCurrentTime", 0f);
                return timer_2;
            case Eras.American:
                float timer_3 = PlayerPrefs.GetFloat("AmericanEraCurrentTime", 0f);
                return timer_3;
            case Eras.Japanese:
                float timer_4 = PlayerPrefs.GetFloat("JapaneseEraCurrentTime", 0f);
                return timer_4;
            case Eras.MartialLaw:
                float timer_5 = PlayerPrefs.GetFloat("MartialLawEraCurrentTime", 0f);
                return timer_5;
            default:
                return 0f;
        }
    }

    private void StartGameTimer(Eras eras)
    {
        if (!allowToCount) return;
        switch (eras)
        {
            case Eras.PreColonial:
                preColonialTimer += Time.deltaTime;
                break;
            case Eras.Spanish:
                spanishEraTimer += Time.deltaTime;
                break;
            case Eras.American:
                americanEraTimer += Time.deltaTime;
                break;
            case Eras.Japanese:
                japaneseEraTimer += Time.deltaTime;
                break;
            case Eras.MartialLaw:
                martialLawEraTimer += Time.deltaTime;
                break;
        }
    }

    public void SaveGameTimer ()
    {
        switch (era)
        {
            case Eras.PreColonial:
                float newRecord_1 = preColonialTimer;
                PlayerPrefs.SetFloat("PreColonialEraCurrentTime", newRecord_1);
                break;
            case Eras.Spanish:
                float newRecord_2 = spanishEraTimer;
                PlayerPrefs.SetFloat("SpanishEraCurrentTime", newRecord_2);
                break;
            case Eras.American:
                float newRecord_3 = americanEraTimer;
                PlayerPrefs.SetFloat("AmericanEraCurrentTime", newRecord_3);
                break;
            case Eras.Japanese:
                float newRecord_4 = japaneseEraTimer;
                PlayerPrefs.SetFloat("JapaneseEraCurrentTime", newRecord_4);
                break;
            case Eras.MartialLaw:
                float newRecord_5 = martialLawEraTimer;
                PlayerPrefs.SetFloat("MartialLawEraCurrentTime", newRecord_5);
                break;
        }
    }

    public void ClearGameTimer (Eras eras)
    {
        switch (eras)
        {
            case Eras.PreColonial:
                PlayerPrefs.SetFloat("PreColonialEraCurrentTime", 0f);
                break;
            case Eras.Spanish:
                PlayerPrefs.SetFloat("SpanishEraCurrentTime", 0f);
                break;
            case Eras.American:
                PlayerPrefs.SetFloat("AmericanEraCurrentTime", 0f);
                break;
            case Eras.Japanese:
                PlayerPrefs.SetFloat("JapaneseEraCurrentTime", 0f);
                break;
            case Eras.MartialLaw:
                PlayerPrefs.SetFloat("MartialLawEraCurrentTime", 0f);
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
        else if (!IsLettersOnly(userName))
        {
            errorInputHandler.text = "Strictly letters only.";
        }
        else
        {
            errorInputHandler.text = "";
            nameInputPanel.SetActive(false);
            PlayerPrefs.SetString("Username", userName);
        }
    }

    static bool IsLettersOnly(string input)
    {
        // Regular expression to match only letters
        Regex regex = new Regex(@"^[a-zA-Z]+$");

        return regex.IsMatch(input);
    }

    public void SetEraFinished()
    {
        switch (era)
        {
            case Eras.PreColonial:
                PlayerPrefs.SetString("FinishedPreColonialEra", "true");
                break;
            case Eras.Spanish:
                PlayerPrefs.SetString("FinishedSpanishEra", "true");
                break;
            case Eras.American:
                PlayerPrefs.SetString("FinishedAmericanEra", "true");
                break;
            case Eras.Japanese:
                PlayerPrefs.SetString("FinishedJapaneseEra", "true");
                break;
            case Eras.MartialLaw:
                PlayerPrefs.SetString("FinishedMartialLawEra", "true");
                break;
        }
    }

    public string GetEraFinished(Eras thisEra)
    {
        switch (thisEra)
        {
            case Eras.PreColonial:
                string era1_ = PlayerPrefs.GetString("FinishedPreColonialEra", "false");
                return era1_;
            case Eras.Spanish:
                string era2_ = PlayerPrefs.GetString("FinishedSpanishEra", "false");
                return era2_;
            case Eras.American:
                string era3_ = PlayerPrefs.GetString("FinishedAmericanEra", "false");
                return era3_;
            case Eras.Japanese:
                string era4_ = PlayerPrefs.GetString("FinishedJapaneseEra", "false");
                return era4_;
            case Eras.MartialLaw:
                string era5_ = PlayerPrefs.GetString("FinishedMartialLawEra", "false");
                return era5_;
            default:
                return "false";
        }
    }

    public void ChangeEra(Eras thisEra)
    {
        era = thisEra;
    }

    public void SetUploadedRecord(Eras thisEra)
    {
        switch (thisEra)
        {
            case Eras.PreColonial:
                PlayerPrefs.SetString("UploadedPreColonialEra", "true");
                break;
            case Eras.Spanish:
                PlayerPrefs.SetString("UploadedSpanishEra", "true");
                break;
            case Eras.American:
                PlayerPrefs.SetString("UploadedAmericanEra", "true");
                break;
            case Eras.Japanese:
                PlayerPrefs.SetString("UploadedJapaneseEra", "true");
                break;
            case Eras.MartialLaw:
                PlayerPrefs.SetString("UploadedMartialLawEra", "true");
                break;
        }
    }

    public bool IsItUploaded (Eras thisEra)
    {
        switch (thisEra)
        {
            case Eras.PreColonial:
                string u_1 = PlayerPrefs.GetString("UploadedPreColonialEra", "false");
                return u_1 == "true";
            case Eras.Spanish:
                string u_2 = PlayerPrefs.GetString("UploadedSpanishEra", "false");
                return u_2 == "true";
            case Eras.American:
                string u_3 = PlayerPrefs.GetString("UploadedAmericanEra", "false");
                return u_3 == "true";
            case Eras.Japanese:
                string u_4 = PlayerPrefs.GetString("UploadedJapaneseEra", "false");
                return u_4 == "true";
            case Eras.MartialLaw:
                string u_5 = PlayerPrefs.GetString("UploadedMartialLawEra", "false");
                return u_5 == "true";
            default:
                return false;
        }
    }

    // Pre Colonial Era Refs
    public void WithLapuLapu(string choice)
    {
        PlayerPrefs.SetString("PreColonial-Chapter3-LapuLapu", choice);
    }

    public void IsWithLapuLapu()
    {
        if (LapuLapu != null)
        {
            string result = PlayerPrefs.GetString("PreColonial-Chapter3-LapuLapu", "n");

            if (result != "n") LapuLapu.SetActive(true);
            else LapuLapu.SetActive(false);
        }
    }

    // Spanish Era Refs
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

    // Martial Law Prefs
    public void PickASide(string side)
    {
        if (side != "")
        {
            PlayerPrefs.SetString("Martial-Law-Side", side);
        }
    }

    public void EndingSelecter(string pick)
    {
        if (pick != "")
        {
            PlayerPrefs.SetString("Martial-Law-EndingSelector", pick);
        }
    } 

    public void PreColonialSaveGame(string savename)
    {
       
        if (savename != "")
        {
            PlayerPrefs.SetString("PreColonial-Era", savename);
        }
    }

    public void JapaneseSaveGame(string savename)
    {

        if (savename != "")
        {
            PlayerPrefs.SetString("Japanese-Era", savename);
        }
    }

    public void MartialLawSaveGame(string savename)
    {
        if (savename != "")
        {
            PlayerPrefs.SetString("MartialLaw-Era", savename);
        }
    }

    public void UpgradeWeapon()
    {
        if (ModeChanger.currentCharacter == "Marco")
        {
            PlayerPrefs.SetString("MarcoGun", "2ndGun");
        }
        else
        {
            PlayerPrefs.SetString("IsabelGun", "2ndGun");
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

    public delegate void Callback();

    public void CheckAndRunThisGameObject(GameObject thisGameObject, Callback callback)
    {
        if (thisGameObject != null)
        {
            callback();
        }
    }

    public void CheckAndRunText(TextMeshProUGUI thisGameObject, Callback callback)
    {
        if (thisGameObject != null)
        {
            callback();
        }
    }

    public enum Eras
    {
        PreColonial,
        Spanish,
        American,
        Japanese,
        MartialLaw,
        None,
    }
}
