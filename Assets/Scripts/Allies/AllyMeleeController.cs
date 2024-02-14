using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllyMeleeController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Chasing")]
    public Transform allyPosition;
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
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private AllyHealthSystem allyHealthSystem;

    [Header("Attack Queue")]
    public Transform currentTargetPos;
    public RaycastHit2D currentHit;
    public GameObject currentlyTargetObj;
    private RaycastHit2D[] detectionRadius;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        allyHealthSystem = GetComponentInParent<AllyHealthSystem>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (EnemyDetected() && !HealthSystem.playerIsDead)
        {
            if (EnemyOnExactRange())
            {
                if (cooldownTimer >= attackCooldown)
                {
                    anim.SetTrigger("isAttacking");
                    cooldownTimer = 0;
                }
                anim.SetBool("isMoving", false);
                if (!allyHealthSystem.allyIsHurt) rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 headThroughPlayer = (currentTargetPos.transform.position - allyPosition.transform.position).normalized * movementSpeed;

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

    public bool EnemyDetected()
    {
        detectionRadius =
            Physics2D.BoxCastAll(detectorCollider.bounds.center + transform.right * detectorRangeX * transform.localScale.x * colliderDistance,
            new Vector3(detectorCollider.bounds.size.x * detectorRangeX, detectorCollider.bounds.size.y * detectorRangeY, detectorCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

        TargetHandler(detectionRadius.Length != 0, detectionRadius);

        return detectionRadius.Length != 0;
    }

    public void TargetHandler(bool onRanged, RaycastHit2D[] hits)
    {
        if (onRanged)
        {
            if (hits.Length <= 1)
            {
                currentTargetPos = hits[0].collider.gameObject.transform;
                currentHit = hits[0];
                currentlyTargetObj = hits[0].collider.gameObject;
            }
            else
            {
                if (!hits.Contains(currentHit))
                {
                    currentHit = hits[0];
                    currentTargetPos = hits[0].collider.gameObject.transform;
                    currentlyTargetObj = hits[0].collider.gameObject;
                }
            }

        }
        else
        {
            currentTargetPos = null;
        }
    }

    public void ChangeTarget(GameObject obj)
    {
        for (int i = 0; i < detectionRadius.Length; i++)
        {
            if (detectionRadius[i].collider.gameObject == obj)
            {
                currentHit = detectionRadius[i];
                currentTargetPos = detectionRadius[i].collider.gameObject.transform;
                currentlyTargetObj = detectionRadius[i].collider.gameObject;
            }
        }
    }

    public bool EnemyOnExactRange()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * stopMovingRangeX * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * stopMovingRangeX, boxCollider.bounds.size.y * stopMovingRangeY, boxCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

        return hit == currentHit;
    }

    // Para kumulay lang yung detection
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * stopMovingRangeX * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * stopMovingRangeX, boxCollider.bounds.size.y * stopMovingRangeY, boxCollider.bounds.size.z));
    }
}
