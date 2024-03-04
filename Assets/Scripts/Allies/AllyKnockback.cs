using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyKnockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float knockbackForce = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySlash"))
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 knockback = -direction * knockbackForce;

            rb.AddForce(knockback, ForceMode2D.Impulse);

        }

        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            // Half the knockback since it's a gun
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 knockback = -direction * (knockbackForce / 2);

            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }
}
