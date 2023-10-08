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

    [Header("Init Facing Direction")]
    [SerializeField] private float directionY = 0f;
    [SerializeField] private float directionX = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        anim.SetFloat("Vertical", directionY);
        anim.SetFloat("Horizontal", directionX);
        
    }

    void Update()
    {
        if(health <= 0)
        {
            deathCallback.Invoke();
            Destroy(this.gameObject, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("isHurt");
    }
}
