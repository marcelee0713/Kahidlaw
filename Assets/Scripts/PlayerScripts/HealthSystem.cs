using System.Collections;
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

    [Header("Hit Effect")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    private PlayerMovement playerMovement;

    private bool deathAnimAlreadyPlayed = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;

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
            if(!deathAnimAlreadyPlayed)
            {
                anim.SetTrigger("isDead");
                deathAnimAlreadyPlayed = true;
            }
            deadPlayerCallback.Invoke();
            playerMovement.DisableGloballyAnyMovements();
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            TakeDamage(5);
        } else if (collision.gameObject.CompareTag("EnemySlash"))
        {
            TakeDamage(10);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        float value = health / maxHealth;
        healthBar.value = value;
        Flash();
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
        spriteRenderer.material = flashMaterial;
        redPanel.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMaterial;
        redPanel.SetActive(false);
        flashRoutine = null;
    }

    public void SetHealth(float extraHealth)
    {
        float thisHealth = health + extraHealth;

        if(thisHealth >= maxHealth)
        {
            health = maxHealth;
            float value = health / maxHealth;
            healthBar.value = value;
        } else
        {
            float value = thisHealth / maxHealth;
            healthBar.value = value;
        }

    }

    public void KillPlayer()
    {
        TakeDamage((int)maxHealth);
    }
}
