using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform[] checkpoints;
    private Transform currentCheckpoint;
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        currentCheckpoint = checkpoints[0];
    }

    public void changeCheckpoint(int checkpointIndex)
    {
        currentCheckpoint = checkpoints[checkpointIndex];
    }

    public void teleportPlayer()
    {
        playerTransform.position = currentCheckpoint.position;
    }
}
