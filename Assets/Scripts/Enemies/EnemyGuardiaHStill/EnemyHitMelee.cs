using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitMelee : MonoBehaviour
{
    private EnemyHealthSystem healthSystem;
    void Start()
    {
        healthSystem = GetComponentInParent<EnemyHealthSystem>();
    }
}
