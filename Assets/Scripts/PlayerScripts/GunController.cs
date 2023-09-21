using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Joystick gunJoyStick;
    private Animator anim;
    public GameObject gunObj;
    public Vector2 aimDirection;

    [Header("Muzzle")]
    public GameObject bullet;
    public float bulletSpeed = 1f;
    public Transform muzzleLocation;
    public Joystick movementJoyStick;

    GameObject projectile;

    void Start()
    {
        anim = GetComponent<Animator>();
        gunObj.SetActive(false);
    }

    void Update()
    {
        if (ModeChanger.currentCharacter != this.gameObject.name)
        {
            return;
        }

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
        if (movementJoyStick.Direction.normalized != Vector2.zero && aimDirection != Vector2.zero)
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
        if (aimDirection != Vector2.zero)
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

    public void Shoot()
    {
        Destroy(projectile);
        projectile = Instantiate(bullet, muzzleLocation.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(aimDirection.x * bulletSpeed, aimDirection.y * bulletSpeed);
        projectile.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);
    }
}
