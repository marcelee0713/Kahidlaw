using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionSeconds = 1f;

    public bool isTransitioning = false;

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

    public void LoadChapter3Prologue()
    {
        StartCoroutine(LoadLevel("Chapter3-Prologue"));
    }

    public void LoadChapter3Start()
    {
        StartCoroutine(LoadLevel("Chapter3-InsideTheDungeon"));
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
