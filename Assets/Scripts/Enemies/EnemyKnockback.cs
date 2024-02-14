using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float knockbackForce = 50f;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 knockback = -direction * knockbackForce;

            rb.AddForce(knockback, ForceMode2D.Impulse);

        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Half the knockback since it's a gun
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 knockback = -direction * (knockbackForce / 2);

            rb.AddForce(knockback, ForceMode2D.Impulse);
        }


        if (collision.gameObject.CompareTag("Player"))
        {
            // Half the knockback since it's a gun
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 knockback = -direction * (knockbackForce / 2);

            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }
}
