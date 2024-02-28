using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CTF : MonoBehaviour
{
    [Header ("UI & Instances")]
    [SerializeField] private GameObject barObject;
    [SerializeField] private Slider bar;
    [SerializeField] private Image fill;
    [SerializeField] private SpriteRenderer flag;
    [SerializeField] private Sprite defaultFlag;
    [SerializeField] private Sprite playerFlag;
    [SerializeField] private Sprite enemyFlag;
    [SerializeField] private float value;
    [SerializeField] private float maxValue;
    [SerializeField] private bool isCaptured = false;

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

    void Start()
    {
        defaultFlag = flag.sprite;
        value = 0f;
        bar.maxValue = maxValue;
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

        if (playerMajority)
        {
            ReCaptureValueHandler(playerMajority, players, enemies);
        }
        else
        {
            ReCaptureValueHandler(enemyMajority, enemies, players);
        }
    }

    private void ReCaptureValueHandler(bool majority, List<GameObject> demensedList, List<GameObject> opposingList)
    {
        // If there is atleast or a lot more than the players
        if (majority && (opposingList.Count > demensedList.Count))
        {
            value -= Time.deltaTime;
            bar.value = value;
        }
        // If there is no longer enemies around the radius of the captured flag
        else if (majority && (opposingList.Count <= 0 || opposingList.Count < demensedList.Count))
        {
            if (value <= maxValue) value += Time.deltaTime;
            bar.value = value;
        }
    }

    private void ValueHandler()
    {
        if (players.Count == enemies.Count) return;

        value += Time.deltaTime;
        bar.value = value;

        if (players.Count > enemies.Count)
        {
            playerMajority = true;
            enemyMajority = false;
        }
        else
        {
            playerMajority = false;
            enemyMajority = true;
        }

    }

    private void ChangeColorRadiusAndFillColor()
    {
        if (players.Count == enemies.Count)
        {
            radiusSr.color = defaultColor;
            fill.color = defaultColor;

            return;
        }

        Color playerColorFill = new Color(playerColor.r, playerColor.g, playerColor.b, 255f);
        Color enemyColorFill = new Color(enemyColor.r, enemyColor.g, enemyColor.b, 255f);


        radiusSr.color = players.Count > enemies.Count ? radiusSr.color = playerColor : radiusSr.color = enemyColor;
        fill.color = players.Count > enemies.Count ? fill.color = playerColorFill : fill.color = enemyColorFill;
    } 

    private void Absence()
    {
        radiusSr.color = defaultColor;
        fill.color = defaultColor;

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
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject obj = collision.gameObject;

            if (enemies.Remove(obj)) enemies.Remove(obj);
        }
    }
}
