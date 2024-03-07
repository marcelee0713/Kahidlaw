using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private GunController gunController;
    private MeleeController meleeController;

    [SerializeField] private bool enableDirection = false;
    [SerializeField] private float faceDirectionX = 0f;
    [SerializeField] private float faceDirectionY = 0f;

    public bool AllowToMoveBoth = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gunController = GetComponent<GunController>();
        meleeController = GetComponent<MeleeController>();

        if(enableDirection)
        {
            anim.SetFloat("Horizontal", faceDirectionX);
            anim.SetFloat("Vertical", faceDirectionY);

        }
    }

    // bao
    void Update()
    {
        if (!AllowToMoveBoth)
        {
            if (DialogueManager.isDialogueActive || QTEManager.QTEIsActive || ModeChanger.currentCharacter != this.gameObject.name)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
                anim.SetBool("isMoving", false);
                if (DialogueManager.isDialogueActive)
                {
                    joystick.DisableInput();
                }

                return;
            }
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

            if (gunController.aimDirection == Vector2.zero)
            {
                anim.SetBool("isMoving", true);
                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.y);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
            anim.SetBool("isMoving", false);

        }
    }

    void UpdateMovements()
    {
        if(meleeController != null)
        {
            bool isAttacking = meleeController.isAttackCooldown;
            if(isAttacking)
            {
                rb.velocity = new Vector2(speed.x * (moveSpeed - 1.5f), speed.y * (moveSpeed - 1.5f));
            }
            else
            {
                rb.velocity = new Vector2(speed.x * moveSpeed, speed.y * moveSpeed);
            }


        } 
        else
        {
            rb.velocity = new Vector2(speed.x * moveSpeed, speed.y * moveSpeed);
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

    public void DisableGloballyAnyMovements()
    {
        joystick.DisableInput();
    }

    public void DoNotAllowToBothMove()
    {
        AllowToMoveBoth = false;
    }
}
