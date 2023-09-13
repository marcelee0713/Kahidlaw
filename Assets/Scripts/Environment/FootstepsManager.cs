using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
    public AudioSource footStepsAudioSource;

    public AudioClip woodFootSteps;
    public AudioClip tilesFootSteps;

    public Joystick joystick;
    public string stepped = "";

    void Update()
    {
        if (ModeChanger.currentCharacter != this.gameObject.name || DialogueManager.isDialogueActive)
        {
            footStepsAudioSource.enabled = false;
            return;
        }
        else
        {
            footStepsAudioSource.enabled = true;
        }

        if (joystick.Direction.normalized != Vector2.zero)
        {
            IsMoving();
        }
        else
        {
            footStepsAudioSource.Stop();
            footStepsAudioSource.enabled = false;

        }
    }

    void IsMoving()
    {
        footStepsAudioSource.enabled = true;

        if (stepped == "wood")
        {
            footStepsAudioSource.clip = woodFootSteps;
        }
        else if (stepped == "tiles")
        {
            footStepsAudioSource.clip = tilesFootSteps;
        }

        if (!footStepsAudioSource.isPlaying)
        {
            footStepsAudioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TileWood"))
        {
            stepped = "wood";
        }
        else if (collision.gameObject.CompareTag("TileFloor"))
        {
            stepped = "tiles";
        }


    }
}
