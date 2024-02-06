using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    private QTEDetails qte;

    [Header("Handlers")]
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI counterText;
    public Button mashButton;
    public Slider bar;

    private UnityAction action;
    private int currentAmount = 0;
    private float currentTime;

    public static bool QTEIsActive = false;

    [SerializeField] private GameObject controller;

    public void StartQTE(QTEDetails initDetails)
    {
        QTEIsActive = true;
        this.gameObject.SetActive(true);

        mashButton.interactable = true;
        currentAmount = 0;
        bar.value = 0;
        bar.maxValue = 0;

        this.qte = initDetails;
        infoText.text = this.qte.message;
        bar.maxValue = this.qte.amountToPress;
        currentTime = this.qte.timer;


        mashButton.onClick.RemoveListener(action);
        mashButton.onClick.AddListener(action);

        controller.SetActive(!QTEIsActive);
    }


    private void Awake()
    {
        if(mashButton.interactable)
        {
            action = () =>
            {
                currentAmount += 1;
                bar.value += 1;
            };
        }
    }

    private void Update()
    {
        if(qte != null)
        {
            if (currentAmount >= qte.amountToPress)
            {
                qte.successCallback.Invoke();
                mashButton.interactable = false;
                this.gameObject.SetActive(false);
                QTEIsActive = false;
                
                if (controller != null) controller.SetActive(!QTEIsActive);
                return;
            }

            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                counterText.text = ((int)Math.Round(currentTime)).ToString();
            }
            else
            {
                counterText.text = 0.ToString();
                qte.failedCallback.Invoke();
                mashButton.interactable = false;
                this.gameObject.SetActive(false);
                QTEIsActive = false;
                if (controller != null) controller.SetActive(!QTEIsActive);
            }
        }
    }
}
