using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialEnvironment : MonoBehaviour
{
    public GameObject Hinter;
    public TextMeshProUGUI hinterText;

    public Guide[] guides;

    public void SetGuideDone (int i)
    {
        guides[i].IsDone = true;
    }

    public void SetHinterText(int i)
    {
        hinterText.text = guides[i].guide;
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
