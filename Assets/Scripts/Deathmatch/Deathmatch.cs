using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Deathmatch : MonoBehaviour
{
    private int isabelValue;
    private int marcoValue;
    private bool hadCalledback = false;

    [Header("Teams")]
    public Teams currentTeam;
    public Sprite isabelImage;
    public Sprite marcoImage;
    public Image currentTeamImage;

    [Header("Teams Healthbars")]
    public Slider marcoSlider;
    public Slider isabelSlider;

    [Header("Alagads or Allies")]
    public GameObject[] isabelsAllies;
    public GameObject[] marcosAllies;

    [Header("Events")]
    public GameObject enemySlainedNotifier;
    public UnityEvent isabelWinsCallback;
    public UnityEvent marcoWinsCallback;

    void Start()
    {
        InitializeDeathMatch();
    }


    void Update()
    {
        if (hadCalledback) return;

        HandleCallbacks();
        HandleHealthBars();
    }

    private void HandleHealthBars()
    {
        marcoValue = ArrayChecker(marcosAllies);
        marcoSlider.value = marcoValue;

        isabelValue = ArrayChecker(isabelsAllies);
        isabelSlider.value = isabelValue;
    } 

    private void HandleCallbacks()
    {
        if (marcoValue <= 0)
        {
            isabelWinsCallback.Invoke();
            hadCalledback = true;
            return; 
        }

        else if (isabelValue <= 0)
        {
            marcoWinsCallback.Invoke();
            hadCalledback = true;
            return;
        }
    }

    private int ArrayChecker(GameObject[] allies)
    {
        int howManyAreAlive = 0;

        for(int i = 0; i < allies.Length; i++) {
            if (allies[i] != null) howManyAreAlive++;
        }

        return howManyAreAlive;
    }

    private void InitializeDeathMatch()
    {
        isabelValue = isabelsAllies.Length;
        marcoValue = marcosAllies.Length;
        isabelSlider.value = isabelValue;
        isabelSlider.maxValue = isabelValue;
        marcoSlider.value = marcoValue;
        marcoSlider.maxValue = marcoValue;

        if (currentTeam.ToString() == "MARCO")
        {
            currentTeamImage.sprite = marcoImage;
        }
        else
        {
            currentTeamImage.sprite = isabelImage;
        }
    }

    public void MarcoWins()
    {
        Debug.Log("Marco Wins");
    }

    public void IsabelWins()
    {
        Debug.Log("Isabel Wins");
    }

    public void ShowEnemySlained()
    {
        StopAllCoroutines();
        StartCoroutine(HandleEnemySlain());
    }

    private IEnumerator HandleEnemySlain()
    {
        enemySlainedNotifier.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        enemySlainedNotifier.SetActive(false);
    }

    public enum Teams
    {
        ISABEL,
        MARCO,
    }
}
