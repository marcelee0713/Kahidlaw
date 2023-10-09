using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningDirectionHandler : MonoBehaviour
{
    [SerializeField] private Animator npcAnim;
    [SerializeField] private float yDirection = 0f;
    [SerializeField] private float xDirection = 0f;

    void Start()
    {
        if (npcAnim != null)
        {
            npcAnim.SetFloat("Vertical", yDirection);
            npcAnim.SetFloat("Horizontal", xDirection);
        }
    }

    void Update()
    {
        
    }
}
