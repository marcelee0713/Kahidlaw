using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour
{
    private EnemyHealthSystem bossHealth;
    [SerializeField] private Slider healthBar;
    void Start()
    {
        bossHealth = GetComponentInParent<EnemyHealthSystem>();
        healthBar.maxValue = bossHealth.health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = bossHealth.health;
    }
}
