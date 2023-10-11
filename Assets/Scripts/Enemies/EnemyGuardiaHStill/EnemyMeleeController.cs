using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Chasing")]
    public Transform playerPosition;
    public Transform enemyPosition;
    public float movementSpeed = 2.5f;

    [Header("Detector Distance")]
    [SerializeField] private BoxCollider2D detectorCollider;
    [SerializeField] private float detectorRangeY;
    [SerializeField] private float detectorRangeX;

    [Header("Hitting Distance")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float stopMovingRangeY;
    [SerializeField] private float stopMovingRangeX;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerDetectected() && !HealthSystem.playerIsDead)
        {
            if (EnemyOnExactRange())
            {
                if (cooldownTimer >= attackCooldown)
                {
                    anim.SetTrigger("isAttacking");
                    cooldownTimer = 0;
                }
                anim.SetBool("isMoving", false);
                rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 headThroughPlayer = (playerPosition.transform.position - enemyPosition.transform.position).normalized * movementSpeed;

                rb.velocity = headThroughPlayer;
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
            rb.velocity = Vector2.zero;
        }

    }

    public bool PlayerDetectected()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(detectorCollider.bounds.center + transform.right * detectorRangeX * transform.localScale.x * colliderDistance,
            new Vector3(detectorCollider.bounds.size.x * detectorRangeX, detectorCollider.bounds.size.y * detectorRangeY, detectorCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    } 

    public bool EnemyOnExactRange()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * stopMovingRangeX * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * stopMovingRangeX, boxCollider.bounds.size.y * stopMovingRangeY, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    // Para kumulay lang yung detection
    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * stopMovingRangeX * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * stopMovingRangeX, boxCollider.bounds.size.y * stopMovingRangeY, boxCollider.bounds.size.z));
    }
}
