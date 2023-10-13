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
    public bool enemyIsHurt = false;

    [Header("Player Damages")]
    [SerializeField] private int slash = 5;
    [SerializeField] private int gun = 2;

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
        enemyIsHurt = true;
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
        enemyIsHurt = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            TakeDamage(slash);
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(gun);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Flash();
    }
}
