using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [Header("Location")]
    public string DefaultLocation;
    public string DefaultEra;
    public TextMeshProUGUI locationText;
    public TextMeshProUGUI eraText;

    [Header("Missions")]
    public Mission[] missions;
    public TextMeshProUGUI[] taskTexts;
    public TextMeshProUGUI missionsFinishedText;
    public TextMeshProUGUI totalMissionsText;
    public GameObject MissionsPanel;
    public static bool isMissionPanelOpen;

    [Header("Notifier")]
    public GameObject notifier;
    public TextMeshProUGUI notifierText;

    void Start()
    {
        isMissionPanelOpen = false;
        MissionsPanel.SetActive(false);
        notifier.SetActive(false);
        locationText.text = DefaultLocation;
        eraText.text = DefaultEra;
        InstantiateMissionsUI();
    }

    // Missions UI
    public void InstantiateMissionsUI()
    {
        totalMissionsText.text = missions.Length.ToString();

        for (int i = 0; i < missions.Length; i++)
        {
            taskTexts[i].text = "- " + missions[i].task;
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
            ShowNotifier();
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

        missionsFinishedText.text = counter.ToString();
    }

    private void ShowNotifier()
    {
        StopAllCoroutines();
        StartCoroutine(HandleNotifier());
    }

    private IEnumerator HandleNotifier()
    {
        notifier.SetActive(true);
        yield return new WaitForSeconds(3f);
        notifier.SetActive(false);

    }
}

[System.Serializable]
public class Mission
{
    public string task;
    public bool IsDone;
}
