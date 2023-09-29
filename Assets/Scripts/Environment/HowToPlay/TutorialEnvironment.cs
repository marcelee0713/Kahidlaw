using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEnvironment : MonoBehaviour
{
    public GameObject Hinter;
    public TextMeshProUGUI hinterText;
    public Guide[] guides;
    public GameObject FinishedDialog;

    public string finishedText;
    public TextMeshProUGUI finishedPlaceholder;
    public Button stayButton;
    public Button nextButton;
    private bool stayed = false;

    private void Start()
    {
        FinishedDialog.SetActive(false);

        stayButton.onClick.AddListener(() =>
        {
            stayed = true;
            FinishedDialog.SetActive(false);
            stayButton.onClick.RemoveAllListeners();
        });

        nextButton.onClick.AddListener(() =>
        {

            stayed = true;
            nextButton.onClick.RemoveAllListeners();
        });
    }

    private void Update()
    {
        if(AreAllGuidesAreFinished() && !stayed)
        {
            finishedPlaceholder.text = finishedText;
            FinishedDialog.SetActive(true);
        }
    }

    public void SetGuideDone (int i)
    {
        guides[i].IsDone = true;
    }

    public void SetHinterText(int i)
    {
        hinterText.text = guides[i].guide;
    }

    public void ChangeText(string text)
    {
        hinterText.text = text;
    }

    public bool AreAllGuidesAreFinished()
    {
        bool handler = true;
        foreach (Guide guide in guides)
        {
            if (!guide.IsDone)
            {
                handler = false;
                break;
            }
        }

        return handler;
    }

    [System.Serializable]
    public class Guide
    {
        public string guide;
        public bool IsDone;
        public int GuideNumber;
    }

    [System.Serializable]
    public class Updater
    {
        public string message;
        public bool IsItDone;
        public int index;
    }

}
