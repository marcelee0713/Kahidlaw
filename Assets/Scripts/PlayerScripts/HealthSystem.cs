using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private Animator anim;
    public Slider healthBar;
    public GameObject redPanel;
    public static bool playerIsDead = false;

    public UnityEvent deadPlayerCallback;

    void Start()
    {
        playerIsDead = false;
        if(redPanel != null)
        {
            redPanel.SetActive(false);
        }
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            playerIsDead = true;
            anim.SetTrigger("isDead");
            deadPlayerCallback.Invoke();
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            TakeDamage(5);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        float value = health / maxHealth;
        healthBar.value = value;

        StopAllCoroutines();
        StartCoroutine(HurtEffect());
    }

    private IEnumerator HurtEffect ()
    {
        redPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        redPanel.SetActive(false);

    }

    public void SetHealth(float extraHealth)
    {
        float thisHealth = health + extraHealth;

        if(thisHealth >= 100)
        {
            health = 100;
        } else
        {
            health += extraHealth;
        }
        float value = health / maxHealth;
        healthBar.value = value;
    }

    public void KillPlayer()
    {
        TakeDamage(100);
    }
}
