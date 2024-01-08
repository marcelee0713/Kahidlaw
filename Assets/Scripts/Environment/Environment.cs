using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    [Header("Location")]
    public string DefaultLocation;
    public string DefaultEra;
    public TextMeshProUGUI locationText;
    public TextMeshProUGUI eraText;

    [Header("Missions Tab")]
    public Mission[] missions;
    public TextMeshProUGUI[] taskTexts;
    public GameObject[] missionsHolder;
    public TextMeshProUGUI missionsFinishedText;
    public TextMeshProUGUI totalMissionsText;
    public GameObject MissionsPanel;
    public static bool isMissionPanelOpen;

    [Header("Notifier")]
    public GameObject notifier;
    public TextMeshProUGUI notifierText;

    [Header("Characters")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Sprite isabelImage;
    [SerializeField] private Sprite marcoImage;

    [Header("Map tab")]
    public GameObject minimap;
    public GameObject marcoCamera;
    public GameObject isabelCamera;

    void Start()
    {
        isMissionPanelOpen = false;
        CheckAndRunGameObject(MissionsPanel, () => MissionsPanel.SetActive(false));
        CheckAndRunGameObject(notifier, () => notifier.SetActive(false));
        CheckAndRunText(locationText, () => locationText.text = DefaultLocation);
        CheckAndRunText(eraText, () => eraText.text = DefaultEra);
        ClearMissionsUI();
        InstantiateMissionsUI();
        CheckAndRunThisImage(characterImage, () => characterImage.sprite = marcoImage);
    }

    public void HandleMinimap()
    {
        if (minimap.activeSelf)
        {
            minimap.SetActive(false);

            return;
        }


        if (ModeChanger.currentCharacter == "Marco")
        {
            CheckAndRunGameObject(marcoCamera, () => marcoCamera.SetActive(true));
            CheckAndRunGameObject(isabelCamera, () => isabelCamera.SetActive(false));
        }
        else
        {
            CheckAndRunGameObject(isabelCamera, () => isabelCamera.SetActive(true));
            CheckAndRunGameObject(marcoCamera, () => marcoCamera.SetActive(false));
        }


        minimap.SetActive(true);
    }

    // Missions UI
    public void InstantiateMissionsUI()
    {
        CheckAndRunText(totalMissionsText, () => totalMissionsText.text = missions.Length.ToString());

        for (int i = 0; i < missions.Length; i++)
        {
            CheckAndRunText(eraText, () =>
            {
                taskTexts[i].text = missions[i].task;
                taskTexts[i].fontStyle = FontStyles.Normal;
                CheckAndRunGameObject(missionsHolder[i], () => missionsHolder[i].SetActive(true));
            });

        }
        UpdateMissionsCount();

    }

    public void ClearMissionsUI()
    {
        for (int i = 0; i < missionsHolder.Length; i++)
        {
            CheckAndRunGameObject(missionsHolder[i], () => missionsHolder[i].SetActive(false));
        }

        for (int i = 0; i < missions.Length; i++)
        {
            taskTexts[i].text = "";
        }
    }

    public void HandleDisplayMission()
    {
        if (!isMissionPanelOpen)
        {
            MissionsPanel.SetActive(true);
            isMissionPanelOpen = true;
        }
        else
        {
            MissionsPanel.SetActive(false);
            isMissionPanelOpen = false;
        }
    }

    public void UpdateMission(int index)
    {
        if (!missions[index].IsDone)
        {
            missions[index].IsDone = true;
            taskTexts[index].fontStyle = FontStyles.Strikethrough;

            UpdateMissionsCount();
        }
    }

    private void UpdateMissionsCount()
    {
        int counter = 0;
        for (int i = 0; i < missions.Length; i++)
        {
            if (missions[i].IsDone)
            {
                counter++;
            }
        }

        CheckAndRunText(missionsFinishedText, () => missionsFinishedText.text = counter.ToString());
    }

    public void ClearMission()
    {
        missions = new Mission[0];
    }

    public void SetMission(string taskName)
    {
        int missionsCount = missions.Length;
        Array.Resize(ref missions, missionsCount);

        Mission newMission = new Mission();
        newMission.task = taskName;
        newMission.IsDone = false;

        missions = new List<Mission>(missions) {newMission}.ToArray();
    } 


    // Notifier UI
    public void ShowNotifier(string notifierNewText)
    {
        StopAllCoroutines();
        StartCoroutine(HandleNotifier(notifierNewText));
    }

    private IEnumerator HandleNotifier(string text)
    {
        notifierText.text = text;
        notifier.SetActive(true);
        yield return new WaitForSeconds(3f);
        notifier.SetActive(false);

    }

    // Location & Era 
    public void ChangeLocation(string newLocation)
    {
        locationText.text = newLocation;
    }

    public void ChangeEra(string newEra)
    {
        eraText.text = newEra;
    }

    // Switching Characters Handler
    public void ChangeCharacterIcon()
    {
        if (ModeChanger.currentCharacter == "Marco")
        {
            characterImage.sprite = marcoImage;
        } else
        {
            characterImage.sprite = isabelImage;
        }
    }

    public delegate void Callback();

    public void CheckAndRunThisImage(Image thisGameObject, Callback callback)
    {
        if (thisGameObject != null)
        {
            callback();
        }
    }

    public void CheckAndRunGameObject(GameObject thisGameObject, Callback callback)
    {
        if (thisGameObject != null)
        {
            callback();
        }
    }

    public void CheckAndRunText(TextMeshProUGUI thisGameObject, Callback callback)
    {
        if (thisGameObject != null)
        {
            callback();
        }
    }

    // Missions
    public bool AreAllMissionFinished()
    {
        bool handler = true;
        foreach (Mission mission in missions)
        {
            if (!mission.IsDone)
            {
                handler = false;
                break;
            }
        }

        return handler;
    }

}

[System.Serializable]
public class Mission
{
    public string task;
    public bool IsDone;
}
