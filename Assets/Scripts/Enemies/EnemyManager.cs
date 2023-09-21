using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public UnityEvent noMoreEnemiesCallback;
    private bool hasInvoked = false;

    void Update()
    {
        if (AllEnemiesAreGone())
        {
            if(!hasInvoked)
            {
                noMoreEnemiesCallback.Invoke();
                hasInvoked = true;
            }
        }
    }

    private bool AllEnemiesAreGone()
    {
        bool handler = true;
        foreach (var enemy in enemies)
        {
            if(enemy != null)
            {
                handler = false;
            }
        }

        return handler;
    }
}
