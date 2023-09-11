using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameObject musicFromStart;
    [Header("Music")]
    public GameObject musicFromMainMenu;
    public AudioSource audioSource;
    public float FadeTime = 1f;

    [Header("Background")]
    public Image background;
    public Sprite mainBackground;
    public Sprite tutorialBackground;

    private void Start()
    {
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

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ChangeMainBackground()
    {
        background.sprite = mainBackground;
    }

    public void ChangeTutorialBackground()
    {
        background.sprite = tutorialBackground;
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
