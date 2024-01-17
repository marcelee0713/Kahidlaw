using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerRefs;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionSeconds = 1f;

    public bool isTransitioning = false;

    [Header("Loading Screen Handlers")]
    [SerializeField] private Image loadingImage;
    [SerializeField] private Eras eras;
    public Sprite preColonialEraImage;
    public Sprite spanishEraImage;
    public Sprite japaneseEraImage;
    public Sprite martialLawEraImage;
    public Sprite justBlackImage;

    private void Awake()
    {
        ChangeLoadingImage();
    }

    public void ChangeLoadingImage()
    {
        switch (eras)
        {
            case Eras.PreColonial:
                loadingImage.sprite = preColonialEraImage;
                break;
            case Eras.Spanish:
                loadingImage.sprite = spanishEraImage;
                break;
            case Eras.American:
                loadingImage.sprite = japaneseEraImage;
                break;
            case Eras.Japanese:
                loadingImage.sprite = japaneseEraImage;
                break;
            case Eras.MartialLaw:
                loadingImage.sprite = martialLawEraImage;
                break;
            case Eras.None:
                loadingImage.sprite = justBlackImage;
                break;
        }
    }

    public void ChangeErasForLoadingScreen(int eraIndex)
    {
        switch(eraIndex) {
            case 0:
                eras = Eras.PreColonial;
                break;
            case 1:
                eras = Eras.Spanish;
                break;
            case 2:
                eras = Eras.Japanese;
                break;
            case 3:
                eras = Eras.American;
                break;
            case 4:
                eras = Eras.MartialLaw;
                break;
            case 5:
                eras = Eras.None;
                break;
        }
    }

    public void LoadFirstTutorial()
    {
        StartCoroutine(LoadLevel("MovementTutorial"));
    }

    public void LoadSecondTutorial()
    {
        StartCoroutine(LoadLevel("CombatTutorial"));
    }

    public void LoadThirdTutorial()
    {
        StartCoroutine(LoadLevel("DialogueTutorial"));
    }

    public void LoadFourthTutorial()
    {
        StartCoroutine(LoadLevel("MissionsTutorial"));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void LoadLastGame()
    {
        string lastSave = PlayerPrefs.GetString("Spanish-Era", "Chapter1-Prologue");

        if (lastSave != "")
        {
            StartCoroutine(LoadLevel(lastSave));
        }
        else
        {
            StartCoroutine(LoadLevel("Chapter1-Prologue"));
        }

    }

    public void LoadChapter1SpanishEraPrologue()
    {
        StartCoroutine(LoadLevel("Chapter1-Prologue"));
    }

    public void LoadNewGameStartSpanishEra()
    {
        StartCoroutine(LoadLevel("Chapter1-Start"));
    }

    public void LoadChapter1OutsideSpanishEra()
    {
        StartCoroutine(LoadLevel("Chapter1-Outside"));
    }


    public void LoadChapter2SpanishEraPrologue()
    {
        StartCoroutine(LoadLevel("Chapter2-Prologue"));
    }

    public void LoadChapter2StartTimeLineSpanishEra()
    {
        StartCoroutine(LoadLevel("Chapter2-CreepyMonce"));
    }

    public void LoadChapter2StartSpanishEra()
    {
        StartCoroutine(LoadLevel("Chapter2-Start"));
    }

    public void LoadChapter2OutsideSpanishEra()
    {
        StartCoroutine(LoadLevel("Chapter2-Outside"));
    }

    public void LoapChapter2AfterOutsideSpanishEra()
    {
        StartCoroutine(LoadLevel("Chapter2-AfterOutside"));
    }

    public void LoapChapter2PadreMonsiAttemption()
    {
        StartCoroutine(LoadLevel("Chapter2-PadreMonsiAttempt"));
    }

    public void LoadChapter2PadreMonsiSexualAssault()
    {
        StartCoroutine(LoadLevel("Chapter2-SexualAssault"));
    }

    public void LoadChapter2IsabelEscaped()
    {
        StartCoroutine(LoadLevel("Chapter2-Escaped"));
    }

    public void LoadChapter2IsabelRaged()
    {
        StartCoroutine(LoadLevel("Chapter2-IsabelRaged"));
    }

    public void LoadChapter2IsabelEscapes()
    {
        StartCoroutine(LoadLevel("Chapter2-IsabelEscapes"));
    }

    public void LoadChapter2SpanishEraEnding()
    {
        StartCoroutine(LoadLevel("Chapter2-EndingOutside"));

    }

    public void LoadChapter3Prologue()
    {
        StartCoroutine(LoadLevel("Chapter3-Prologue"));
    }

    public void LoadChapter3Start()
    {
        StartCoroutine(LoadLevel("Chapter3-InsideTheDungeon"));
    }

    public void LoadChapter3AfterDungeon()
    {
        StartCoroutine(LoadLevel("Chapter3-AfterDungeon"));
    }

    public void LoadChapter3TheMeetUp()
    {
        StartCoroutine(LoadLevel("Chapter3-TheMeetUp"));
    }

    public void LoadChapter3StartGuerrillas()
    {
        StartCoroutine(LoadLevel("Chapter3-StartGuerrillasPrologue"));
    }

    public void LoadChapter3GuerrillasPlace()
    {
        StartCoroutine(LoadLevel("Chapter3-GuerrillasPlace"));
    }

    public void LoadChapter3InsideGuerrillasHouse()
    {
        StartCoroutine(LoadLevel("Chapter3-InsideGuerillaHouse"));
    }

    public void LoadChapter3GuerrillaAmbush()
    {
        StartCoroutine(LoadLevel("Chapter3-GuerrillaAmbush"));
    }

    public void LoadChapter3GuerillaFight()
    {
        StartCoroutine(LoadLevel("Chapter3-GuerillaFight"));
    }

    public void LoadChapter3GuerillaPostFight()
    {
        StartCoroutine(LoadLevel("Chapter3-GuerillaPostFight"));
    }

    public void LoadChapter3TrapHouse()
    {
        StartCoroutine(LoadLevel("Chapter3-TrapHouse"));
    }

    public void LoadChapter3FinalFight()
    {
       StartCoroutine(LoadLevel("Chapter3-FinalFight"));
    }

    public void LoadChapter3PadreMonsiCaught()
    {
        StartCoroutine(LoadLevel("Chapter3-PadreMonsiCaught"));
    }

    public void LoadChapter3FinalConsqeuences()
    {
        string hasBeenAssaulted = PlayerPrefs.GetString("Chapter2-Assaulted", "y");

        if (hasBeenAssaulted == "n")
        {
            StartCoroutine(LoadLevel("Chapter3-FinalGuerrillaPrologue"));
        } else
        {
            StartCoroutine(LoadLevel("Chapter3-IsabelKillsMonsi"));
        }
    }

    public void LoadChapter3BadEndingPrologue()
    {
        StartCoroutine(LoadLevel("Chapter3-BadEndingPrologue"));
    }

    public void LoadChapter3GuerrillaCelebration()
    {
        StartCoroutine(LoadLevel("Chapter3-GuerrillaCelebration"));
    }

    public void LoadChapter3PadreMonsiLockedUp()
    {
        StartCoroutine(LoadLevel("Chapter3-PadreMonsiLockedUp"));
    }

    public void LoadChapter3EndingMessage()
    {
        StartCoroutine(LoadLevel("Chapter3-EndingMessage"));
    }

    public void LoadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string currentNameScene = currentScene.name;
        StartCoroutine(LoadLevel(currentNameScene));
    }

    IEnumerator LoadLevel (string scene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionSeconds);

        SceneManager.LoadScene(scene);
    }
}
