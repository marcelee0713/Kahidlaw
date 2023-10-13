using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDirectionOnRanged : MonoBehaviour
{
    public bool inRangedOn4d = false;
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Horizontal Detectors")]
    [SerializeField] private LeftDetector LDetector;
    [SerializeField] private RightDetector RDetector;

    [Header("Vertical Detectors")]
    [SerializeField] private FrontDetector FDetector;
    [SerializeField] private BackDetector BDetector;

    [Header("For While Running Shooting")]
    public Transform playerPosition;
    public Transform enemyPosition;
    public float movementSpeed = 5f;
    public bool allowToMove = false;

    [Header("Transition Distance")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float stopMovingRangeY;
    [SerializeField] private float stopMovingRangeX;
    [SerializeField] private LayerMask playerLayer;


    [Header("Shooting Distance")]
    [SerializeField] private float shootingDistance;
    [SerializeField] private BoxCollider2D shootBoxCollider;
    [SerializeField] private float shootRangeY;
    [SerializeField] private float shootRangeX;

    [SerializeField] private EnemyHealthSystem enemyHealthSystem;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        enemyHealthSystem = GetComponentInParent<EnemyHealthSystem>();
    }

    private void Update()
    {
        if (HealthSystem.playerIsDead)
        {
            inRangedOn4d = false;
            anim.SetBool("isShooting", false);
            rb.velocity = Vector2.zero;
            if (allowToMove) anim.SetBool("isWShooting", false);

            return;
        }

        if(EnemyOnShootingRange())
        {

            if (allowToMove)
            {
                if (EnemyOnExactRange())
                {
                    if (!enemyHealthSystem.enemyIsHurt) rb.velocity = Vector2.zero;
                    anim.SetBool("isShooting", true);
                    anim.SetBool("isWShooting", false);
                }
                else
                {
                    anim.SetBool("isWShooting", true);
                    Vector2 headThroughPlayer = (playerPosition.transform.position - enemyPosition.transform.position).normalized * movementSpeed;

                    rb.velocity = headThroughPlayer;
                }

            } 
            else
            {

                if (!enemyHealthSystem.enemyIsHurt)
                {
                    rb.velocity = Vector2.zero;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                } 
                else
                {
                    rb.constraints = RigidbodyConstraints2D.None;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                }

                anim.SetBool("isShooting", true);
            }

        }
        else
        {
            inRangedOn4d = false;
            anim.SetBool("isShooting", false);
            rb.velocity = Vector2.zero;
            if (allowToMove) anim.SetBool("isWShooting", false);
        }


    }

    public bool EnemyOnShootingRange()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(shootBoxCollider.bounds.center + transform.right * shootRangeX * transform.localScale.x * shootingDistance,
            new Vector3(shootBoxCollider.bounds.size.x * shootRangeX, shootBoxCollider.bounds.size.y * shootRangeY, shootBoxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        inRangedOn4d = hit.collider != null;

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
        if(allowToMove)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * stopMovingRangeX * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * stopMovingRangeX, boxCollider.bounds.size.y * stopMovingRangeY, boxCollider.bounds.size.z));
        }
    }
}
