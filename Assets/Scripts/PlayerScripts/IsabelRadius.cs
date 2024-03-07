using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsabelRadius : MonoBehaviour
{
    [SerializeField] private PlayerMovement isabelMovement;
    [SerializeField] private PlayerMovement marcoMovement;
    [SerializeField] private bool onRadius = true;
    [SerializeField] private bool turnOn = true;

    [Header("Dialogues")]
    [SerializeField] private GameObject isabelInfo;

    private void Update()
    {
        if(turnOn)
        {
            marcoMovement.AllowToMoveBoth = onRadius;
            isabelMovement.AllowToMoveBoth = onRadius;
            return;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(turnOn && collision.gameObject.name == "Isabel")
        {
            onRadius = true;
            isabelInfo.SetActive(!onRadius);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Isabel")
        {
            onRadius = false;
            if (turnOn)
            {
                isabelInfo.SetActive(!onRadius);
            }

        }
    }

    public void TurnOff()
    {
        turnOn = false;
        onRadius = false;
    }

    public void TurnOn()
    {
        turnOn = true;
    }
}
