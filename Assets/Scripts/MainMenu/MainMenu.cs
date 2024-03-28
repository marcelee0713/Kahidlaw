using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static PlayerRefs;
using System;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    private GameObject musicFromStart;
    [Header("Music")]
    public GameObject musicFromMainMenu;
    public AudioSource audioSource;
    public float FadeTime = 1f;

    [Header("Backgrounds")]
    public Image background;
    public Sprite mainBackground;
    public Sprite tutorialBackground;
    public Sprite storyModeBackground;
    public Sprite leaderboardsBackground;

    [Header("Player State")]
    [SerializeField] private PlayerRefs playerRefs;
    [SerializeField] private DatabaseManager db;
    public GameObject eraInformation;
    public GameObject recordInfoContainer;
    public GameObject uploadableInfoContainer;
    public GameObject eraNotAvailableContainer;
    public TextMeshProUGUI eraNotAvailableText;
    public TextMeshProUGUI eraText;
    public TextMeshProUGUI currentTimeRecordText;
    public TextMeshProUGUI completedTimeRecordText;
    public Image currentEraImage;
    public Sprite[] eraImages;

    [Header("Playable Buttons")]
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private GameObject newGamePanel;
    private float defaultContinuePosX;
    private float defaultContinuePosY;
    private float defaultNewPosX;
    private float defaultNewPosY;
    [SerializeField] private GameObject continueGameObj;
    [SerializeField] private GameObject newGameObj;
    [SerializeField] private GameObject uploadBtnGameObj;
    [SerializeField] private TextMeshProUGUI recordInfoText;

    // This is just for the UI bug
    private bool isUploadableContainer = false;

    private void Start()
    {
        defaultContinuePosX = continueGameObj.transform.localPosition.x;
        defaultContinuePosY = continueGameObj.transform.localPosition.y;
        defaultNewPosX = newGameObj.transform.localPosition.x;
        defaultNewPosY = newGameObj.transform.localPosition.y;
        ChangeMainBackground();

        musicFromStart = GameObject.FindGameObjectWithTag("GameAudio");
        if (musicFromStart != null)
        {
            audioSource = musicFromStart.GetComponent<AudioSource>();
        }
        else
        {
            audioSource = musicFromMainMenu.GetComponent<AudioSource>();
        }
    }

    // For Testing
    private void UnlockAll()
    {
        PlayerPrefs.SetString("FinishedPreColonialEra", "true");
        PlayerPrefs.SetString("FinishedSpanishEra", "true");
        PlayerPrefs.SetString("FinishedAmericanEra", "true");
        PlayerPrefs.SetString("FinishedJapaneseEra", "true");
        PlayerPrefs.SetString("FinishedMartialLawEra", "true");
    }

    // Main Menu | Initialize Game Related

    public void OpenEra(int selectedEraIndex)
    {
        switch (selectedEraIndex)
        {
            case 0:
                playerRefs.ChangeEra(Eras.PreColonial);
                eraInformation.SetActive(true);
                ChangeButtonPositionsAndStates(Eras.PreColonial);
                ChangeTimeRecords(Eras.PreColonial);
                db.ChangeCollection("PreColonialEra");
                currentEraImage.sprite = eraImages[0];
                break;

            case 1:
                if (CheckIfFinished("FinishedPreColonialEra", "Pre-Colonial Era"))
                {
                    playerRefs.ChangeEra(Eras.Spanish);
                    eraInformation.SetActive(true);
                    ChangeButtonPositionsAndStates(Eras.Spanish);
                    ChangeTimeRecords(Eras.Spanish);
                    db.ChangeCollection("SpanishEra");
                    currentEraImage.sprite = eraImages[1];
                    break;
                }

                eraNotAvailableContainer.SetActive(true);
                break;

            case 2:
                if (CheckIfFinished("FinishedSpanishEra", "Spanish Era"))
                {
                    playerRefs.ChangeEra(Eras.American);
                    eraInformation.SetActive(true);
                    ChangeButtonPositionsAndStates(Eras.American);
                    ChangeTimeRecords(Eras.American);
                    db.ChangeCollection("AmericanEra");
                    currentEraImage.sprite = eraImages[2];
                    break;
                }

                eraNotAvailableContainer.SetActive(true);
                break;

            case 3:
                if (CheckIfFinished("FinishedSpanishEra", "Spanish Era"))
                {
                    playerRefs.ChangeEra(Eras.Japanese);
                    eraInformation.SetActive(true);
                    ChangeButtonPositionsAndStates(Eras.Japanese);
                    ChangeTimeRecords(Eras.Japanese);
                    db.ChangeCollection("JapaneseEra");
                    currentEraImage.sprite = eraImages[2];
                    break;
                }

                eraNotAvailableContainer.SetActive(true);
                break;

            case 4:
                if (CheckIfFinished("FinishedJapaneseEra", "Japanese Era"))
                {
                    playerRefs.ChangeEra(Eras.MartialLaw);
                    eraInformation.SetActive(true);
                    ChangeButtonPositionsAndStates(Eras.MartialLaw);
                    ChangeTimeRecords(Eras.MartialLaw);
                    db.ChangeCollection("MartialLawEra");
                    currentEraImage.sprite = eraImages[3];
                    break;
                }

                eraNotAvailableContainer.SetActive(true);
                break;

            default:
                break;
        }
    }

    public void ContinueGameAction ()
    {
        switch (playerRefs.era)
        {
            case Eras.PreColonial:
                levelLoader.LoadLastGame("PreColonial-Era", "PreColonial-Chapter1-Prologue");
                FadeOutMusic();
                break;
            case Eras.Spanish:
                levelLoader.LoadLastGame("Spanish-Era", "Chapter1-Prologue");
                FadeOutMusic();
                break;
            case Eras.American:
                // Add a LoadLastGame for American
                FadeOutMusic();
                break;
            case Eras.Japanese:
                levelLoader.LoadLastGame("Japanese-Era", "Japanese-Chapter1-Prologue");
                FadeOutMusic();
                break;
            case Eras.MartialLaw:
                levelLoader.LoadLastGame("MartialLaw-Era", "Japanese-Chapter1-Prologue");
                FadeOutMusic();
                break;
        }
    }
    
    public void NewGameAction()
    {
        switch (playerRefs.era)
        {
            case Eras.PreColonial:
                if (HadPlayed("PreColonial-Era", "PreColonialEraCurrentTime")) break;

                StartNewGame();
                break;
            case Eras.Spanish:
                if (HadPlayed("Spanish-Era", "SpanishEraCurrentTime")) break;

                StartNewGame();
                break;
            case Eras.American:
                if (HadPlayed("American-Era", "AmericanEraCurrentTime")) break;

                StartNewGame();
                break;
            case Eras.Japanese:
                if (HadPlayed("Japanese-Era", "JapaneseEraCurrentTime")) break;

                StartNewGame();
                break;
            case Eras.MartialLaw:
                if (HadPlayed("MartialLaw-Era", "MartialLawEraCurrentTime")) break;

                StartNewGame();
                break;
        }
    }

    private bool HadPlayed(string saveStatePrefName, string currentTimePrefName)
    {
        string savedState_01 = PlayerPrefs.GetString(saveStatePrefName, "false");
        float currentTime_01 = PlayerPrefs.GetFloat(currentTimePrefName, 0f);

        if (currentTime_01 <= 0f || (savedState_01 == "false" || savedState_01 == ""))
        {
            return false;
        }

        if (uploadableInfoContainer.activeSelf && !recordInfoContainer.activeSelf) 
        {
            isUploadableContainer = true;
            uploadableInfoContainer.SetActive(false);
        }
        else
        {
            isUploadableContainer = false;
            recordInfoContainer.SetActive(false);
        }

        newGamePanel.SetActive(true);
        return true;

    }

    public void OnNo()
    {
        if (isUploadableContainer)
        {
            uploadableInfoContainer.SetActive(true);
        }
        else
        {
            recordInfoContainer.SetActive(true);
        }
    }

    public void StartNewGame()
    {
        PlayerPrefs.SetString("MarcoGun", "1stGun");
        PlayerPrefs.SetString("IsabelGun", "1stGun");

        switch (playerRefs.era)
        {
            case Eras.PreColonial:
                ResetEra("PreColonial-Era", "PreColonialEraCurrentTime", "FinishedPreColonialEra", "UploadedPreColonialEra");
                ResetEra("Spanish-Era", "SpanishEraCurrentTime", "FinishedSpanishEra", "UploadedSpanishEra");
                ResetEra("American-Era", "AmericanEraCurrentTime", "FinishedAmericanEra", "UploadedAmericanEra");
                ResetEra("Japanese-Era", "JapaneseEraCurrentTime", "FinishedJapaneseEra", "UploadedJapaneseEra");
                ResetEra("MartialLaw-Era", "MartialLawEraCurrentTime", "FinishedMartialLawEra", "UploadedMartialLawEra");

                levelLoader.LoadGame("PreColonial-Chapter1-Prologue"); 
                FadeOutMusic();
                break;
            case Eras.Spanish:
                ResetEra("Spanish-Era", "SpanishEraCurrentTime", "FinishedSpanishEra", "UploadedSpanishEra");
                ResetEra("American-Era", "AmericanEraCurrentTime", "FinishedAmericanEra", "UploadedAmericanEra");
                ResetEra("Japanese-Era", "JapaneseEraCurrentTime", "FinishedJapaneseEra", "UploadedJapaneseEra");
                ResetEra("MartialLaw-Era", "MartialLawEraCurrentTime", "FinishedMartialLawEra", "UploadedMartialLawEra");

                levelLoader.LoadGame("Chapter1-Prologue");
                FadeOutMusic();
                break;
            case Eras.American:
                ResetEra("American-Era", "AmericanEraCurrentTime", "FinishedAmericanEra", "UploadedAmericanEra");
                ResetEra("Japanese-Era", "JapaneseEraCurrentTime", "FinishedJapaneseEra", "UploadedJapaneseEra");
                ResetEra("MartialLaw-Era", "MartialLawEraCurrentTime", "FinishedMartialLawEra", "UploadedMartialLawEra");

                // Make a function for the start of American;
                FadeOutMusic();
                break;
            case Eras.Japanese:
                ResetEra("Japanese-Era", "JapaneseEraCurrentTime", "FinishedJapaneseEra", "UploadedJapaneseEra");
                ResetEra("MartialLaw-Era", "MartialLawEraCurrentTime", "FinishedMartialLawEra", "UploadedMartialLawEra");

                levelLoader.LoadGame("Japanese-Chapter1-Prologue");
                FadeOutMusic();
                break;
            case Eras.MartialLaw:
                ResetEra("MartialLaw-Era", "MartialLawEraCurrentTime", "FinishedMartialLawEra", "UploadedMartialLawEra");

                levelLoader.LoadGame("MartialLaw-Chapter1-Prologue");
                FadeOutMusic();
                break;
        }
    }

    private void ResetEra(string saveStatePrefName, string currentTimePrefName, string finishPrefName, string uploadedPrefName)
    {
        PlayerPrefs.SetString(saveStatePrefName, "");
        PlayerPrefs.SetFloat(currentTimePrefName, 0f);
        PlayerPrefs.SetString(finishPrefName, "");
        PlayerPrefs.SetString(uploadedPrefName, "");
    }

    private void ChangeButtonPositionsAndStates (Eras era)
    {
        if (playerRefs.GetEraFinished(era) == "true")
        {
            uploadableInfoContainer.SetActive(true);
            recordInfoContainer.SetActive(false);
            continueGameObj.SetActive(false);
            newGameObj.SetActive(true);
            newGameObj.transform.localPosition = new Vector3(0, defaultNewPosY);
        }
        else
        {
            int gameTime = Mathf.FloorToInt(playerRefs.GetGameTImer(era));

            if (gameTime <= 0)
            {
                newGameObj.SetActive(true);
                newGameObj.transform.localPosition = new Vector3(0, defaultNewPosY);
                continueGameObj.SetActive(false);
            }
            else
            {
                newGameObj.transform.localPosition = new Vector3(defaultNewPosX, defaultNewPosY);
                continueGameObj.transform.localPosition = new Vector3(defaultContinuePosX, defaultContinuePosY);
                continueGameObj.SetActive(true);
                newGameObj.SetActive(true);
            }

            uploadableInfoContainer.SetActive(false);
            recordInfoContainer.SetActive(true);
        }
    }

    private void ChangeTimeRecords (Eras era)
    {
        int totalTime_0 = Mathf.FloorToInt(playerRefs.GetGameTImer(era));
        TimeSpan timeSpan_0 = TimeSpan.FromSeconds(totalTime_0);
        string formattedTime_0 = $"{(int)timeSpan_0.TotalHours:D2}:{timeSpan_0.Minutes:D2}:{timeSpan_0.Seconds:D2}";
        currentTimeRecordText.text = "Current Time Record: " + formattedTime_0;
        completedTimeRecordText.text = "Current Time Record: " + formattedTime_0;

        if (playerRefs.IsItUploaded(era))
        {
            uploadBtnGameObj.SetActive(false);
            recordInfoText.text = "Checkout your current record on the leaderboards!";
        }
        else
        {
            uploadBtnGameObj.SetActive(true);
            recordInfoText.text = "You have finished the era! Upload your record so everybody can also see it!";

        }
    }
    
    private bool CheckIfFinished (string eraPrefName, string selectedEra)
    {
        string isFinished = PlayerPrefs.GetString(eraPrefName, "false");

        eraNotAvailableText.text = "You have to finish " + selectedEra + " first!";

        return isFinished == "true";

    }

    // Main Menu | Background Picture Related
    public void ChangeMainBackground()
    {
        background.sprite = mainBackground;
    }

    public void ChangeStoryModeBackground()
    {
        background.sprite = storyModeBackground;
    }

    public void ChangeTutorialBackground()
    {
        background.sprite = tutorialBackground;
    }

    public void ChangeLeaderboardBackground()
    {
        background.sprite = leaderboardsBackground;
    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOutCoreMusic());
    }

    private IEnumerator FadeOutCoreMusic()
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0f)
        {
            var tmp = audioSource.volume;
            audioSource.volume = tmp - (startVolume * Time.deltaTime / FadeTime);
            yield return new WaitForEndOfFrame();
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
        Destroy(musicFromStart);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
