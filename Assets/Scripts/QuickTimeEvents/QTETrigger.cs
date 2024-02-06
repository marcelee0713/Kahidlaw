using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QTETrigger : MonoBehaviour
{
    [Header("QTE Detail")]
    public QTEDetails qteDetail;
    public GameObject QTEBox;

    public void InitiateQTE()
    {
        QTEBox.SetActive(true);
        FindObjectOfType<QTEManager>().StartQTE(qteDetail);
    }

    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}

[System.Serializable]
public class QTEDetails
{
    public float timer;
    public string message;
    public UnityEvent successCallback;
    public UnityEvent failedCallback;
    public int amountToPress;
}
