using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthSystem : MonoBehaviour
{
    public int health = 20;
    public UnityEvent deathCallback;
    private Animator anim;
    private Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            deathCallback.Invoke();
            Destroy(this.gameObject, 1f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            TakeDamage(5);
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(2);
        }

    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("isHurt");
    }
}
