using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningDirectionHandler : MonoBehaviour
{
    [SerializeField] private Animator npcAnim;
    [SerializeField] private float yDirection = 0f;
    [SerializeField] private float xDirection = 0f;

    private void Awake()
    {
        if (npcAnim != null)
        {
            npcAnim.SetFloat("Vertical", yDirection);
            npcAnim.SetFloat("Horizontal", xDirection);
        }
    }
}
