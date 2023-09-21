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

    public void LoadNewGameStartSpanishEra()
    {
        StartCoroutine(LoadLevel("Chapter1-Start"));
    }

    public void LoadChapter1OutsideSpanishEra()
    {
        StartCoroutine(LoadLevel("Chapter1-Outside"));
    }

    IEnumerator LoadLevel (string scene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionSeconds);

        SceneManager.LoadScene(scene);
    }
}
