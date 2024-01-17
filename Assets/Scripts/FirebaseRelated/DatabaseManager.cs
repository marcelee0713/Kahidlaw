using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerRefs;

public class DatabaseManager : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] private string databaseCollection;
    [SerializeField] private Button submitButton;
    [SerializeField] private PlayerRefs playerRefs;

    [Header("Upload State")]
    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject error;
    [SerializeField] private GameObject success;

    [Header("Leaderboards & State")]
    public TextMeshProUGUI selectedEraText;
    public TextMeshProUGUI[] playerNamesText;
    public TextMeshProUGUI[] playerRecordsText;
    public GameObject leaderBoardError;

    private void Start()
    {
        ChangeEra(0);
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(() =>
            {
                string isFinished = playerRefs.GetEraFinished(playerRefs.era);
                if (isFinished == "false") return;
                SubmitRecord();
            });
        }

    }

    public async Task<List<UserFinishedEraData>> GetMyCollections()
    {
        List<UserFinishedEraData> list = new List<UserFinishedEraData>();

        var db = FirebaseFirestore.DefaultInstance;
        Query spanishEraQuery = db.Collection(databaseCollection);

        try
        {

            QuerySnapshot allSpanishEraQuerySnapshot = await spanishEraQuery.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in allSpanishEraQuerySnapshot.Documents)
            {
                Dictionary<string, object> datas = documentSnapshot.ToDictionary();

                string name = "";
                string totalTime = "";


                foreach (var value in datas.Values)
                {
                    if (name == "") name = value.ToString();
                    else totalTime = value.ToString();
                }

                UserFinishedEraData obj = new UserFinishedEraData(name, totalTime);
                list.Add(obj);
            }

            list = list.OrderBy(x => int.Parse(x.totalTimeInSeconds)).ToList();

            if(list.Count > 5) list = list.Take(5).ToList();

            return list;

        }
        catch
        {
            // Show no internet connection in the UI
            Debug.Log("may error pre");
            list.Clear();
            leaderBoardError.SetActive(true);
            return list;
        }

    }

    public void ChangeCollection(string firebaseCollectionName)
    {
        databaseCollection = firebaseCollectionName;
    }

    public async void ChangeEra (int eraIndex)
    {
        Debug.Log("I got pressed");
        ResetTexts();
        switch (eraIndex)
        {
            case 0:
                playerRefs.era = Eras.PreColonial;
                ChangeCollection("PreColonialEra");
                selectedEraText.text = "Pre-Colonial Era";
                List<UserFinishedEraData> preColonialList = await GetMyCollections();
                ShowRecords(preColonialList);
                break;
            case 1:
                playerRefs.era = Eras.Spanish;
                ChangeCollection("SpanishEra");
                selectedEraText.text = "Spanish Era";
                List<UserFinishedEraData> spanishList = await GetMyCollections();
                ShowRecords(spanishList);
                break;
            case 2:
                playerRefs.era = Eras.American;
                ChangeCollection("AmericanEra");
                selectedEraText.text = "American Era";
                List<UserFinishedEraData> americanList = await GetMyCollections();
                ShowRecords(americanList);
                break;
            case 3:
                playerRefs.era = Eras.Japanese;
                ChangeCollection("JapaneseEra");
                selectedEraText.text = "Japanese Era";
                List<UserFinishedEraData> japaneseList = await GetMyCollections();
                ShowRecords(japaneseList);
                break;
            case 4:
                playerRefs.era = Eras.MartialLaw;
                ChangeCollection("MartialLawEra");
                selectedEraText.text = "Martial Law Era";
                List<UserFinishedEraData> martialLawList = await GetMyCollections();
                ShowRecords(martialLawList);
                break;
        }
    }

    public void ResetTexts ()
    {
        foreach(TextMeshProUGUI name in playerNamesText)
        {
            name.text = "";
        }

        foreach(TextMeshProUGUI record in playerRecordsText)
        {
            record.text = "";
        } 
    }

    public void ShowRecords(List<UserFinishedEraData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            playerNamesText[i].text = list[i].name;
            playerRecordsText[i].text = FormatTime(int.Parse(list[i].totalTimeInSeconds));
        }
    }

    public string FormatTime(int time)
    {
        TimeSpan timeSpan_0 = TimeSpan.FromSeconds(time);
        string formattedTime_0 = $"{(int)timeSpan_0.TotalHours:D2}:{timeSpan_0.Minutes:D2}:{timeSpan_0.Seconds:D2}";
        return formattedTime_0;
    }

    public void SubmitRecord()
    {
        string userName = PlayerPrefs.GetString("Username", "Unknown");
        string totalTime = Mathf.FloorToInt(playerRefs.GetGameTImer(playerRefs.era)).ToString();

        var recordData = new UserFinishedEraData
        {
            name = userName,
            totalTimeInSeconds = totalTime,
        };

        var firestore = FirebaseFirestore.DefaultInstance;
        var collectionRef = firestore.Collection(databaseCollection).Document();
        string id = collectionRef.Id;

        firestore.Document(databaseCollection + "/" + id).SetAsync(recordData).ContinueWithOnMainThread(task =>
        {
            loading.SetActive(true);
            error.SetActive(false);
            success.SetActive(false);

            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Failed to set document: " + task.Exception);
                loading.SetActive(false);
                error.SetActive(true);
                success.SetActive(false);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Document set successfully!");
                loading.SetActive(false);
                error.SetActive(false);
                success.SetActive(true);
                playerRefs.SetUploadedRecord(playerRefs.era);
            }
        });
    }
}