using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusisChecker : MonoBehaviour
{
    private GameObject musicFromStart;
    private AudioSource thisAudioSource;

    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        musicFromStart = GameObject.FindGameObjectWithTag("GameAudio");

        if(musicFromStart == null)
        {
            thisAudioSource.Play();
        }

    }
}
