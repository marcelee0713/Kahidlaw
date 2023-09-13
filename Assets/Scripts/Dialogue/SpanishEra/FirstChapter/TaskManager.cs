using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Environment gameManager;

    [Header("Collectible Section")]
    public string collectableName = "";
    public int collectableMissionIndex = 0;
    public int currentCollectables = 0;
    public int totalCollectables;
    public GameObject[] collectibles;


    private void Start()
    {
        totalCollectables = collectibles.Length;
    }

    public void UpdateCollectables()
    {
        currentCollectables = currentCollectables + 1;

        if (currentCollectables == totalCollectables)
        {
            gameManager.UpdateMission(collectableMissionIndex);
            gameManager.ShowNotifier("Task Finished");
        }
        else
        {
            gameManager.ShowNotifier($"{collectableName}: {currentCollectables} / {totalCollectables}");

        }
    }

    public void UpdateFinishedTask(int index)
    {
        gameManager.UpdateMission(index);
    }

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
