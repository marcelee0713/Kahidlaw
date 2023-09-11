using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public int FadeTime = 1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FadeOutBackgroundMusic()
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
    }
}
