using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    [Header("Hit Effect")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        anim.SetFloat("Vertical", directionY);
        anim.SetFloat("Horizontal", directionX);

        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }

    void Update()
    {
        if(health <= 0)
        {
            deathCallback.Invoke();
            spriteRenderer.material = flashMaterial;
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
        Flash();
    }
}
