using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public int FadeTime = 1;
    public float volume = 0.2f;

    [SerializeField]
    private AudioClip intenseMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    public void ChangeMusicToIntense()
    {
        audioSource.clip = intenseMusic; 
    }

    public void FadeInBackgroundMusic()
    {
        StartCoroutine(FadeInCoreMusic());
    }

    public void FadeOutBackgroundMusic()
    {
        StartCoroutine(FadeOutCoreMusic());
    }

    private IEnumerator FadeInCoreMusic()
    {
        audioSource.Play();
        while (audioSource.volume < volume)
        {
            var tmp = audioSource.volume;
            audioSource.volume = tmp + (volume * Time.deltaTime / FadeTime);
            yield return new WaitForEndOfFrame();
        }

        audioSource.volume = volume;
    }

    private IEnumerator FadeOutCoreMusic()
    {
        float startVolume = volume;
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
