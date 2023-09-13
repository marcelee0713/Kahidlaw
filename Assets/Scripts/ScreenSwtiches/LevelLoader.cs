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
        StartCoroutine(LoadLevel(3));
    }

    public void LoadThirdTutorial()
    {
        StartCoroutine(LoadLevel(4));
    }

    public void LoadFourthTutorial()
    {
        StartCoroutine(LoadLevel(5));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadNewGameStartSpanishEra()
    {
        StartCoroutine(LoadLevel(2));
    }

    IEnumerator LoadLevel (int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionSeconds);

        SceneManager.LoadScene(levelIndex);
    }
}
