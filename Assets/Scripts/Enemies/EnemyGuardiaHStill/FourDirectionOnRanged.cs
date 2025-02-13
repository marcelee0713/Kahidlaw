using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField] private int halfHealth;
    private bool isHalfed = false;

    [Header("Attack Queue")]
    public Transform currentTargetPos;
    public RaycastHit2D currentHit;
    public GameObject currentlyTargetObj;
    private RaycastHit2D[] detectionRadius;
    public bool allowDifferentMovesets = false;

    [Header("Capture The Flag")]
    [SerializeField] private Transform flagLocation;
    public bool CTFEnable = false;

    [SerializeField] private BoxCollider2D ctfCollider;
    [SerializeField] private float ctfDetectionRangeY;
    [SerializeField] private float ctfDetectionRangeX;
    [SerializeField] private LayerMask ctfLayer;


    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        enemyHealthSystem = GetComponentInParent<EnemyHealthSystem>();
        halfHealth = enemyHealthSystem.health / 2;
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
            anim.SetBool("isMoving", false);

            if (allowToMove)
            {
                Movesets();
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
            if (!CTFEnable)
            {
                StandStill();
                return;
            }

            CaptureTheFlag();

        }
    }

    private void CaptureTheFlag()
    {
        if (!OnCTFRange())
        {
            anim.SetBool("isWShooting", false);
            anim.SetBool("isShooting", false);
            anim.SetBool("isMoving", true);
            Vector2 headThroughFlag = (flagLocation.transform.position - enemyPosition.transform.position).normalized * movementSpeed;
            rb.velocity = headThroughFlag;
            anim.SetFloat("Horizontal", rb.velocity.x);
            anim.SetFloat("Vertical", rb.velocity.y);

        }
        else
        {
            StandStill();
        }
    }

    public bool EnemyOnShootingRange()
    {
        detectionRadius =
            Physics2D.BoxCastAll(shootBoxCollider.bounds.center + transform.right * shootRangeX * transform.localScale.x * shootingDistance,
            new Vector3(shootBoxCollider.bounds.size.x * shootRangeX, shootBoxCollider.bounds.size.y * shootRangeY, shootBoxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        inRangedOn4d = detectionRadius.Length != 0;

        TargetHandler(inRangedOn4d, detectionRadius);

        return detectionRadius.Length != 0;
    }

    public bool EnemyOnExactRange()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * stopMovingRangeX * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * stopMovingRangeX, boxCollider.bounds.size.y * stopMovingRangeY, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    public bool OnCTFRange()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(ctfCollider.bounds.center + transform.right * ctfDetectionRangeX * transform.localScale.x * colliderDistance,
            new Vector3(ctfCollider.bounds.size.x * ctfDetectionRangeX, ctfCollider.bounds.size.y * ctfDetectionRangeY, ctfCollider.bounds.size.z),
            0, Vector2.left, 0, ctfLayer);

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
        for (int i = 0 ; i < detectionRadius.Length; i++)
        {
            if (detectionRadius[i].collider.gameObject == obj)
            {
                currentHit = detectionRadius[i];
                currentTargetPos = detectionRadius[i].collider.gameObject.transform;
                currentlyTargetObj = detectionRadius[i].collider.gameObject;
            }
        }
    }

    // Movesets

    private void Movesets()
    {
        if (!allowDifferentMovesets)
        {
            ForwardWhileShooting();
            return;
        }

        if (halfHealth <= enemyHealthSystem.health)
        {
            ForwardWhileShooting();
        }
        else
        {
            if (!isHalfed)
            {
                stopMovingRangeX += 10f;
                stopMovingRangeY += 3f;
                isHalfed = true;
            }
            BackWhileShooting();
        }
    }
    private void ForwardWhileShooting()
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
            Vector2 headThroughPlayer = (currentTargetPos.transform.position - enemyPosition.transform.position).normalized * movementSpeed;
            rb.velocity = headThroughPlayer;
        }
    }

    private void BackWhileShooting()
    {
        if (EnemyOnExactRange())
        {
            anim.SetBool("isShooting", true);
            anim.SetBool("isWShooting", true);
            Vector2 headThroughPlayer = (currentTargetPos.transform.position - enemyPosition.transform.position).normalized * movementSpeed;
            rb.velocity = -headThroughPlayer;
        }
        else
        {
            anim.SetBool("isShooting", true);
            anim.SetBool("isWShooting", false);
            rb.velocity = Vector2.zero;
        }

    }

    private void StandStill()
    {
        inRangedOn4d = false;
        anim.SetBool("isShooting", false);
        rb.velocity = Vector2.zero;
        if (allowToMove) anim.SetBool("isWShooting", false);
        anim.SetBool("isMoving", false);
    }
}
