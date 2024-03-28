using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpRandomer : MonoBehaviour
{
    [SerializeField] private GameObject[] powerups;

    // Start is called before the first frame update
    void Start()
    {
        powerups[GenerateRandomIndex(powerups.Length)].SetActive(true);
    }

    int GenerateRandomIndex(int length)
    {
        return Random.Range(0, length);
    }
}
