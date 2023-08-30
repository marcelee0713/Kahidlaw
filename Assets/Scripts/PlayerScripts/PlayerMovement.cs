using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    Vector2 movement;
    public Joystick joystick;

    private Vector2 speed;

    public bool isAttacking = false;
    public float attackCd = 0.5f;

    public Environment gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // bao
    void Update()
    {
        if (DialogueManager.isDialogueActive)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
            anim.SetBool("isMoving", false);
            return;
        }
        movement.x = joystick.Horizontal * moveSpeed;
        movement.y = joystick.Vertical * moveSpeed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TestKitchen"))
        {
            gameManager.UpdateMission(1);
            gameManager.UpdateMissionsCount();
            gameManager.ShowNotifier();
        }
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
