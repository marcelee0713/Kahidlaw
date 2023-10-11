using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb2d;
    public GameObject meleeObj;
    public Button meleeButton;
    public float attackCooldown = 0.5f;
    public bool isAttackCooldown = false;
    private Direction currentDirection;

    private float lastHorizontalDirection = 0f;
    private float lastVerticalDirection = -1f;

    private Coroutine attackRoutine;

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        meleeObj.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (rb2d)
        {
            Vector2 velocity = rb2d.velocity;

            float angle = Mathf.Atan2(velocity.y, velocity.x);

            float degrees = Mathf.Rad2Deg * angle;

            currentDirection = CalculateDirection(degrees);
        }
    }

    void Update()
    {
        if (ModeChanger.currentCharacter != this.gameObject.name) return;

        if (ModeChanger.mode == "Melee")
        {
            meleeObj.SetActive(true);
            meleeButton.interactable = rb2d.velocity == Vector2.zero;
        }
        else
        {
            meleeObj.SetActive(false);

        }

        if(rb2d.velocity != Vector2.zero)
        {
            switch (currentDirection)
            {
                case Direction.Up:
                    lastHorizontalDirection = 0f;
                    lastVerticalDirection = 1f;
                    break;
                case Direction.Down:
                    lastHorizontalDirection = 0f;
                    lastVerticalDirection = -1f;
                    break;
                case Direction.Left:
                    lastHorizontalDirection = -1f;
                    lastVerticalDirection = 0f;
                    break;
                case Direction.Right:
                    lastHorizontalDirection = 1f;
                    lastVerticalDirection = 0f;
                    break;
                case Direction.UpLeft:
                    lastHorizontalDirection = -0.5f;
                    lastVerticalDirection = 0.5f;
                    break;
                case Direction.UpRight:
                    lastHorizontalDirection = 0.5f;
                    lastVerticalDirection = 0.5f;
                    break;
                case Direction.DownLeft:
                    lastHorizontalDirection = -0.5f;
                    lastVerticalDirection = -0.5f;
                    break;
                case Direction.DownRight:
                    lastHorizontalDirection = 0.5f;
                    lastVerticalDirection = -0.5f;
                    break;
            }
        }

    }

    private Direction CalculateDirection(float degrees)
    {
        if (degrees < 0)
            degrees += 360;

        if (degrees >= 22.5f && degrees < 67.5f)
            return Direction.UpRight;
        else if (degrees >= 67.5f && degrees < 112.5f)
            return Direction.Up;
        else if (degrees >= 112.5f && degrees < 157.5f)
            return Direction.UpLeft;
        else if (degrees >= 157.5f && degrees < 202.5f)
            return Direction.Left;
        else if (degrees >= 202.5f && degrees < 247.5f)
            return Direction.DownLeft;
        else if (degrees >= 247.5f && degrees < 292.5f)
            return Direction.Down;
        else if (degrees >= 292.5f && degrees < 337.5f)
            return Direction.DownRight;
        else
            return Direction.Right;
    }

    public void Attack()
    {
        
        if(!isAttackCooldown)
        {
            if(attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
            }

            attackRoutine = StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        isAttackCooldown = true;
        anim.SetFloat("Horizontal", lastHorizontalDirection);
        anim.SetFloat("Vertical", lastVerticalDirection);

        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackCooldown);
        anim.SetBool("isAttacking", false);
        isAttackCooldown = false;
        attackRoutine = null;
    }
}
