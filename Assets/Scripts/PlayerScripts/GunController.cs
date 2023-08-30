using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Joystick gunJoyStick;
    private Animator anim;
    public GameObject gunObj;
    Vector2 aimDirection;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gunObj.SetActive(false);
    }

    void Update()
    {
        if (ModeChanger.mode == "Gun")
        {
            onDialogue();
            anim.SetBool("isGunMode", true);
            aimDirection = gunJoyStick.Direction.normalized;
            ShootingAnimation();
            ShootingWhileWalkingAnimation();
        }
        else
        {
            anim.SetBool("isGunMode", false);
            gunObj.SetActive(false);
            anim.SetBool("isShooting", false);
            anim.SetBool("isWShooting", false);

        }

    }

    void onDialogue()
    {
        if (DialogueManager.isDialogueActive)
        {
            gunObj.SetActive(false);
        }
        else
        {
            gunObj.SetActive(true);
        }
    }

    void ShootingWhileWalkingAnimation()
    {
        if (rb.velocity != Vector2.zero && aimDirection != Vector2.zero)
        {
            anim.SetBool("isWShooting", true);
        }
        else
        {
            anim.SetBool("isWShooting", false);
        }
    }

    void ShootingAnimation()
    {
        if(aimDirection != Vector2.zero)
        {
            anim.SetFloat("Horizontal", aimDirection.x);
            anim.SetFloat("Vertical", aimDirection.y);
            anim.SetBool("isShooting", true);
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }


}