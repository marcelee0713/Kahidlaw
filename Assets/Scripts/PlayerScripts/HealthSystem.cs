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

    [Header("Enemy Damages")]
    [SerializeField] private float enemyProjectiles = 5f;
    [SerializeField] private float enemySlash = 10f;
    public float flameDamage = 2f;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    private PlayerMovement playerMovement;
    private bool deathAnimAlreadyPlayed = false;

    private bool isHurt = false;

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
            TakeDamage((int)enemyProjectiles);

        }
        else if (collision.gameObject.CompareTag("EnemySlash"))
        {
            TakeDamage((int)enemySlash);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isHurt)
        {
            health -= damage;
            float value = health / maxHealth;
            healthBar.value = value;

            Flash();
        }
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
        isHurt = true;
        spriteRenderer.material = flashMaterial;
        redPanel.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMaterial;
        redPanel.SetActive(false);
        flashRoutine = null;
        isHurt = false;
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
