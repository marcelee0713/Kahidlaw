using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionSeconds = 1f;

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel (int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionSeconds);

        SceneManager.LoadScene(levelIndex);
    }
}
