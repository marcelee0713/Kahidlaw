using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioIdentifier : MonoBehaviour
{
    public TextMeshProUGUI sfxDash;
    public TextMeshProUGUI backgroundMusicDash;

    private void Start()
    {
        InstantiateBGMusicAndSFXDash();
    }

    public void InstantiateBGMusicAndSFXDash()
    {
        string BgMusicMuted = PlayerPrefs.GetString("BackgroundMusic", "false");
        string sfxMuted = PlayerPrefs.GetString("SFX", "false");

        if (BgMusicMuted == "true")
        {
            backgroundMusicDash.text = "/";
        }
        else
        {
            backgroundMusicDash.text = "";
        }

        if (sfxMuted == "true")
        {
            sfxDash.text = "/";
        }
        else
        {
            sfxDash.text = "";
        }
    }

    public void ToggleSFXDash ()
    {
        string sfxMuted = PlayerPrefs.GetString("SFX", "false");

        if (sfxMuted == "true")
        {
            sfxDash.text = "";
            PlayerPrefs.SetString("SFX", "false");
        }
        else
        {
            sfxDash.text = "/";
            PlayerPrefs.SetString("SFX", "true");
        }
    }

    public void ToggleBGMusicDash()
    {
        string BgMusicMuted = PlayerPrefs.GetString("BackgroundMusic", "false");

        if (BgMusicMuted == "true")
        {
            backgroundMusicDash.text = "";
            PlayerPrefs.SetString("BackgroundMusic", "false");
        }
        else
        {
            backgroundMusicDash.text = "/";
            PlayerPrefs.SetString("BackgroundMusic", "true");
        }
    }
}
