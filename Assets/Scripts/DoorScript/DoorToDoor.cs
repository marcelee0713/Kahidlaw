using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToDoor : MonoBehaviour
{
    [SerializeField] private Transform firstDoorPosition;
    [SerializeField] private Transform secondDoorPosition;
    [SerializeField] private Transform playerPosition;

    public void GoToFirstDoor ()
    {
        playerPosition.position = firstDoorPosition.position;
    }

    public void GoToSecondDoor()
    {
        playerPosition.position = secondDoorPosition.position;
    }
}
