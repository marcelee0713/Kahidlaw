using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerRefs;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private string databaseCollection;
    [SerializeField] private Button submitButton;
    [SerializeField] private PlayerRefs playerRefs;

    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject error;
    [SerializeField] private GameObject success;


    private void Start()
    {
        if(submitButton != null)
        {
            submitButton.onClick.AddListener(() =>
            {
                string isFinished = playerRefs.GetEraFinished(playerRefs.era);
                if (isFinished == "false") return;
                SubmitRecord();
            });
        }

    }

    public async void GetMyCollections()
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

            foreach (var item in list)
            {
                Debug.Log(item);
            }

        }
        catch
        {
            // Show no internet connection in the UI
            Debug.Log("may error pre");
        }

    }

    public void ChangeCollection(string firebaseCollectionName)
    {
        databaseCollection = firebaseCollectionName;
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