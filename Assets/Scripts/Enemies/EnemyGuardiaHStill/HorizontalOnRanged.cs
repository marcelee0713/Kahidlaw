using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalOnRanged : MonoBehaviour
{
    public bool inRangedEnemy  = false;
    private Animator anim;

    [Header("Horizontal Detectors")]
    [SerializeField] private LeftDetector LDetector;
    [SerializeField] private RightDetector RDetector;

    [Header("Vertical Detectors")]
    [SerializeField] private FrontDetector FDetector;
    [SerializeField] private BackDetector BDetector;

    private void Update()
    {
        
        if (LDetector != null && LDetector.targetOnLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (RDetector != null && RDetector.targetOnRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (FDetector != null && FDetector.targetOnFront)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (BDetector != null && BDetector.targetOnBack)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HealthSystem.playerIsDead)
        {
            inRangedEnemy = false;
            anim.SetBool("isShooting", false);
        }

        if (collision.gameObject.CompareTag("Player") && !HealthSystem.playerIsDead)
        {
            inRangedEnemy = true;
            anim.SetBool("isShooting", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRangedEnemy = false;
            anim.SetBool("isShooting", false);
        }
    }
}
