using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CTF : MonoBehaviour
{
    [Header ("UI & Instances")]
    [SerializeField] private GameObject barObject;
    [SerializeField] private Slider bar;
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI flagTextInfo;
    [SerializeField] private SpriteRenderer flag;
    [SerializeField] private Sprite defaultFlag;
    [SerializeField] private Sprite playerFlag;
    [SerializeField] private Sprite enemyFlag;
    [SerializeField] private float value;
    [SerializeField] private float maxValue;
    [SerializeField] private bool isCaptured = false;
    [SerializeField] private bool capturedFlagInit = false;


    [Header ("Current Majority")]
    public bool playerMajority = false;
    public bool enemyMajority = false;

    [Header ("Flag Radius Related")]
    [SerializeField] private SpriteRenderer radiusSr;
    [SerializeField] private Color playerColor;
    [SerializeField] private Color enemyColor;
    [SerializeField] private Color defaultColor;

    [Header ("Teams")]
    [SerializeField] private List<GameObject> players;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private bool hadCalledBack = false;

    [Header("Callbacks")]
    [SerializeField] private UnityEvent playerCapturedCallback;
    [SerializeField] private UnityEvent enemyCapturedCallback;

    private bool haveStarted = false;

    void Start()
    {
        if (!haveStarted)
        {
            defaultFlag = flag.sprite;
            value = 0f;
            bar.value = value;
            bar.maxValue = maxValue;

            haveStarted = true;
            barObject.SetActive(false);
        }

        if (capturedFlagInit)
        {
            value = maxValue;
        }

    }

    void Update ()
    {

        if (!isCaptured)
        {
            Captured();
            CapturingTheFlag();
        }
        else
        {
            Callbacks();
            ReCaptured();
            ReCapturingTheFlag();
        }
    }

    private void CapturingTheFlag ()
    {
        if (players.Count != 0 || enemies.Count != 0)
        {
            ValueHandler();
            ChangeColorRadiusAndFillColor();
        }
        else
        {
            Absence();
        }
    }

    private void ReCapturingTheFlag() 
    {
        if (players.Count == enemies.Count)
        {
            CheckAndRun(barObject, () => flagTextInfo.text = playerMajority ? "defend your flag" : "enemy's flag");
            return;
        }

        if (playerMajority)
        {
            ReCaptureValueHandler(playerMajority, players, enemies);
            //CheckAndRun(barObject, () => flagTextInfo.text = "re-capturing the flag");
        }
        else
        {
            ReCaptureValueHandler(enemyMajority, enemies, players);
            //CheckAndRun(barObject, () => flagTextInfo.text = "the enemy is re-capturing the flag");
        }
    }

    private void ReCaptureValueHandler(bool majority, List<GameObject> demensedList, List<GameObject> opposingList)
    {
        // If there is atleast or a lot more than the players
        if (majority && (opposingList.Count > demensedList.Count))
        {
            value -= Time.deltaTime;
            CheckAndRun(barObject, () => bar.value = value);
            CheckAndRun(barObject, () => flagTextInfo.text = playerMajority ? "the enemy re-capturing the flag" : "re-capturing the flag");

        }

        // If there is no longer enemies around the radius of the captured flag
        else if (majority && (opposingList.Count <= 0 || opposingList.Count < demensedList.Count))
        {
            if (value <= maxValue) value += Time.deltaTime;
            CheckAndRun(barObject, () => bar.value = value);
            CheckAndRun(barObject, () => flagTextInfo.text = "");
        }
    }

    private void ValueHandler()
    {
        if (players.Count == enemies.Count)
        {
            CheckAndRun(barObject, () => flagTextInfo.text = "both teams are equally competing the flag!");
            return;
        }

        value += Time.deltaTime;
        CheckAndRun(barObject, () => bar.value = value);

        if (players.Count > enemies.Count)
        {
            CheckAndRun(barObject,() => flagTextInfo.text = "capturing the flag");
            playerMajority = true;
            enemyMajority = false;
        }
        else
        {
            CheckAndRun(barObject, () => flagTextInfo.text = "the enemy is capturing the flag");
            playerMajority = false;
            enemyMajority = true;
        }

    }

    private void ChangeColorRadiusAndFillColor()
    {
        if (players.Count == enemies.Count)
        {
            radiusSr.color = defaultColor;
            CheckAndRun(barObject, () => fill.color = defaultColor);

            return;
        }

        Color playerColorFill = new Color(playerColor.r, playerColor.g, playerColor.b, 255f);
        Color enemyColorFill = new Color(enemyColor.r, enemyColor.g, enemyColor.b, 255f);


        radiusSr.color = players.Count > enemies.Count ? radiusSr.color = playerColor : radiusSr.color = enemyColor;
        CheckAndRun(barObject, () => fill.color = players.Count > enemies.Count ? fill.color = playerColorFill : fill.color = enemyColorFill);

        
    } 

    private void Absence()
    {
        radiusSr.color = defaultColor;
        CheckAndRun(barObject, () => fill.color = defaultColor);
        CheckAndRun(barObject, () => flagTextInfo.text = "");

        if (value != 0f) value -= Time.deltaTime;

    }

    private void Captured()
    {
        if (value >= maxValue)
        {
            isCaptured = true;
        }
    }

    private void ReCaptured()
    {
        if (value <= 0)
        {
            isCaptured = false;
            hadCalledBack = false;
            flag.sprite = defaultFlag;
        }
    }

    private void Callbacks()
    {
        if (hadCalledBack) return;

        if (playerMajority && !enemyMajority)
        {
            playerCapturedCallback.Invoke();
            hadCalledBack = true;
            flag.sprite = playerFlag;
        }
        else
        {
            enemyCapturedCallback.Invoke();
            hadCalledBack = true;
            flag.sprite = enemyFlag;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject obj = collision.gameObject;

            if (!players.Contains(obj)) players.Add(obj);

            if (collision.gameObject.name == "Marco" || collision.gameObject.name == "Isabel")
            {
                if(!DialogueManager.isDialogueActive) barObject.SetActive(true);
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject obj = collision.gameObject;

            if (!enemies.Contains(obj)) enemies.Add(obj);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject obj = collision.gameObject;

            if (players.Contains(obj)) players.Remove(obj);


            if (collision.gameObject.name == "Marco" || collision.gameObject.name == "Isabel")
            {
               if (haveStarted) barObject.SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject obj = collision.gameObject;

            if (enemies.Remove(obj)) enemies.Remove(obj);
        }
    }

    public delegate void Callback();
    public void CheckAndRun(GameObject thisGameObject, Callback callback)
    {
        if (thisGameObject != null)
        {
            callback();
        }
    }
}
