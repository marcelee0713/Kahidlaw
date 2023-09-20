using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
    public AudioSource footStepsAudioSource;

    public AudioClip woodFootSteps;
    public AudioClip tilesFootSteps;
    public AudioClip brickFootSteps;
    public AudioClip dirtFootSteps;

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

        switch (stepped)
        {
            case "wood":
                footStepsAudioSource.clip = woodFootSteps;
                break;
            case "tiles":
                footStepsAudioSource.clip = tilesFootSteps;
                break;
            case "brick":
                footStepsAudioSource.clip = brickFootSteps;
                break;
            case "dirt":
                footStepsAudioSource.clip = dirtFootSteps;
                break;
        }

        if (!footStepsAudioSource.isPlaying)
        {
            footStepsAudioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "TileWood":
                stepped = "wood";
                break;
            case "TileFloor":
                stepped = "tiles";
                break;
            case "TileBrick":
                stepped = "brick";
                break;
            case "TileDirt":
                stepped = "dirt";
                break;
        }
    }
}
