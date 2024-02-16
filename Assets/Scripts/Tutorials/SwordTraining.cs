using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwordTraining : MonoBehaviour
{
    [SerializeField] private GameObject[] barrels;
    [SerializeField] private UnityEvent successCallback;
    [SerializeField] private UnityEvent unsuccessfulCallback;

    public bool haveBeenHit = false;

    private bool alreadyCalled = false;


    void Update()
    {
        if (haveBeenHit) BarrelStates();
    }

    void BarrelStates()
    {
        if (NoMoreBarrels())
        {
            if (!alreadyCalled)
            {
                successCallback.Invoke();
                alreadyCalled = true;
            }
            Debug.Log("Success");
        }
        else
        {
            if (!alreadyCalled)
            {
                unsuccessfulCallback.Invoke();
                alreadyCalled = true;
            }
            Debug.Log("Failed");
        }
    }

    public bool NoMoreBarrels()
    {
        bool result = true;
        for(int i = 0; i < barrels.Length; i++)
        {
            if (barrels[i] != null)
            {
                result = false;
                break;
            }
        }

        return result;
    }
}
