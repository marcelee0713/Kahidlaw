using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDetector : MonoBehaviour
{
    public bool targetOnFront = false;

    [Header("Front Detector Distance")]
    [SerializeField] private BoxCollider2D detectorCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float detectorRangeY;
    [SerializeField] private float detectorRangeX;
    [SerializeField] private LayerMask detectionLayer;

    [Header("Enemy Related")]
    [SerializeField] private FourDirectionOnRanged onRanged;
    [SerializeField] private EnemyMeleeController onRangedMelee;

    [Header("Ally Related")]
    [SerializeField] private AllyMeleeController onRangedAllyMelee;
    [SerializeField] private AllyGunController onRangedAlly;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
        detectorCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(TargetDetected())
        {
            anim.SetFloat("Horizontal", 0f);
            targetOnFront = true;
            anim.SetFloat("Vertical", -1f);
        }
        else
        {
            targetOnFront = false;
        }
    }


    public bool TargetDetected()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(detectorCollider.bounds.center + transform.right * detectorRangeX * transform.localScale.x * colliderDistance,
            new Vector3(detectorCollider.bounds.size.x * detectorRangeX, detectorCollider.bounds.size.y * detectorRangeY, detectorCollider.bounds.size.z),
            0, Vector2.left, 0, detectionLayer);

        // Enemy Range Weapon Detection
        if (onRanged != null)
        {
            if (onRanged.currentlyTargetObj == null || hit.collider == null) return false;

            return hit.collider.gameObject == onRanged.currentlyTargetObj;
        }

        // Enemy Melee Weapon Detection
        if (onRangedMelee != null)
        {
            if (onRangedMelee.currentlyTargetObj == null || hit.collider == null) return false;


            return hit.collider.gameObject == onRangedMelee.currentlyTargetObj;
        }

        if (onRangedAllyMelee != null)
        {
            if (onRangedAllyMelee.currentlyTargetObj == null || hit.collider == null) return false;


            return hit.collider.gameObject == onRangedAllyMelee.currentlyTargetObj;
        }

        if (onRangedAlly != null)
        {
            if (onRangedAlly.currentlyTargetObj == null || hit.collider == null) return false;


            return hit.collider.gameObject == onRangedAlly.currentlyTargetObj;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(detectorCollider.bounds.center + transform.right * detectorRangeX * transform.localScale.x * colliderDistance,
            new Vector3(detectorCollider.bounds.size.x * detectorRangeX, detectorCollider.bounds.size.y * detectorRangeY, detectorCollider.bounds.size.z));
    }
}
