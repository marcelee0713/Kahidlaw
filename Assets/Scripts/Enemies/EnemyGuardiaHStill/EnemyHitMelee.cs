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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            healthSystem.TakeDamage(5);
        }
    }

}
