using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Pag public pre makikita mo ito doon sa right side bar ng "Player Object"
    public float moveSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    Vector2 movement;
    public Joystick joystick;

    private Vector2 speed;

    public bool isAttacking = false;
    public float attackCd = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // bao
    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.isActive)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
            anim.SetBool("isMoving", false);
            return;
        }
        // Y axis or pa vertical pre na position
        movement.x = joystick.Horizontal * moveSpeed;
        // X axis or pa horizontal pre na position
        movement.y = joystick.Vertical * moveSpeed;
        // 
        speed = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed).normalized;

        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        if (speed != Vector2.zero)
        {
            UpdateMovements();
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetBool("isMoving", true);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
            anim.SetBool("isMoving", false);

        }
    }

    void UpdateMovements()
    {
        rb.velocity = new Vector2(speed.x * moveSpeed, speed.y * moveSpeed);
    }

    public void StartAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        anim.SetBool("isShooting", true);
        yield return new WaitForSeconds(attackCd);
        anim.SetBool("isShooting", false);
        isAttacking = false;

    }
}
