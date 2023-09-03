using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Environment gameManager;

    public void FinishFirstTask()
    {
        gameManager.UpdateMission(0);
        gameManager.ChangeEra("Spanish Era");
    }

    public void FinishSecondTask()
    {
        gameManager.UpdateMission(1);
        gameManager.ChangeLocation("Kanoy's House");
    }
}
