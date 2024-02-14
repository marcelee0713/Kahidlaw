using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllyHealthSystem : MonoBehaviour
{
    public int health = 20;
    public UnityEvent deathCallback;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private AllyMeleeController allyMeleeController;

    [Header("Init Facing Direction")]
    [SerializeField] private float directionY = 0f;
    [SerializeField] private float directionX = 0f;

    [Header("Hit Effect")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    public bool allyIsHurt = false;

    [Header("Enemy Damages")]
    [SerializeField] private int slash = 5;
    [SerializeField] private int gun = 2;

    [Header("Rage State")]
    public bool enableRage = false;
    private int rageHealth;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        anim.SetFloat("Vertical", directionY);
        anim.SetFloat("Horizontal", directionX);

        originalMaterial = spriteRenderer.material;
        rageHealth = health / 2;
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        allyIsHurt = true;
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
        allyIsHurt = false;
    }

    void Update()
    {
        if (health <= 0)
        {
            deathCallback.Invoke();
            spriteRenderer.material = flashMaterial;
            Destroy(this.gameObject, 1f);
        }

        if (enableRage) RageChecker();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            TakeDamage(gun);
        }

        if (collision.gameObject.CompareTag("EnemySlash"))
        {
            TakeDamage(slash);
            if (allyMeleeController) allyMeleeController.ChangeTarget(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!allyIsHurt)
        {
            health -= damage;
            Flash();
        }

    }

    private void RageChecker()
    {
        if (health <= rageHealth)
        {
            anim.SetBool("onStageTwo", true);
        }
    }
}
