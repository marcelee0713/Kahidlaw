using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public struct UserFinishedEraData
{
    [FirestoreProperty]
    public string name { get; set; }

    [FirestoreProperty]
    public string totalTimeInSeconds { get; set; }

    public UserFinishedEraData(string thisName, string totalTimeStr) {
        name = thisName;
        totalTimeInSeconds = totalTimeStr; 
    }
}
