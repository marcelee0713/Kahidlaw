using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDetector : MonoBehaviour
{

    public bool playerIsOnLeft = false;

    [Header("Left Detector Distance")]
    [SerializeField] private BoxCollider2D detectorCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float detectorRangeY;
    [SerializeField] private float detectorRangeX;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private FourDirectionOnRanged onRanged;

    private Animator anim;


    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
        detectorCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (PlayerDetectected())
        {
            anim.SetFloat("Vertical", 0f);
            playerIsOnLeft = true;
            anim.SetFloat("Horizontal", -1f);
        }
        else
        {
            playerIsOnLeft = false;
        }
    }


    public bool PlayerDetectected()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(detectorCollider.bounds.center + transform.right * detectorRangeX * transform.localScale.x * colliderDistance,
            new Vector3(detectorCollider.bounds.size.x * detectorRangeX, detectorCollider.bounds.size.y * detectorRangeY, detectorCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (onRanged.currentlyTargetObj == null || hit.collider == null) return false;

        return hit.collider.gameObject == onRanged.currentlyTargetObj;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(detectorCollider.bounds.center + transform.right * detectorRangeX * transform.localScale.x * colliderDistance,
            new Vector3(detectorCollider.bounds.size.x * detectorRangeX, detectorCollider.bounds.size.y * detectorRangeY, detectorCollider.bounds.size.z));
    }

}
