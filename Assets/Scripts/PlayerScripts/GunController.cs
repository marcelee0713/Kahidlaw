using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Joystick gunJoyStick;
    private Animator anim;
    public GameObject gunObj;
    Vector2 aimDirection;

    void Start()
    {
        anim = GetComponent<Animator>();
        gunObj.SetActive(false);
    }

    void Update()
    {
        if (ModeChanger.mode == "Gun")
        {
            gunObj.SetActive(true);
            anim.SetBool("isGunMode", true);
            aimDirection = gunJoyStick.Direction.normalized;
            ShootingAnimation();
        }
        else
        {
            anim.SetBool("isGunMode", false);
            gunObj.SetActive(false);
            anim.SetBool("isShooting", false);
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
