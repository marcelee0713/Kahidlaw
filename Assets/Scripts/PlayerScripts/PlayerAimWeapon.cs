using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform gunAimTransform;
    public Joystick myStick;
    private GameObject Body;
    private SpriteRenderer bodySr;
    private Rigidbody2D rb;
    private Vector3 lastAimLocalScale;
    private Vector3 lastAimEulerAngle;

    private string direction;


    void Start()
    {
        gunAimTransform = transform.Find("GunMode");
        rb = GetComponent<Rigidbody2D>();
        Body = GameObject.Find("Body");
        bodySr = Body.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 aimDirection = myStick.Direction.normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        HandleDirection();
        HandleMovementWithAim(angle);
    }

    void HandleMovementWithAim(float angle)
    {

        HandleFlipping(angle);



        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {

            HandleDirectionMovement(angle);
        }
        else
        {
            if (angle != 0)
            {
                gunAimTransform.eulerAngles = new Vector3(0, 0, angle);

            }
            else
            {
                gunAimTransform.localScale = lastAimLocalScale;
                gunAimTransform.localEulerAngles = lastAimEulerAngle;
            }
        }

    }

    void HandleDirection()
    {
        if(!(rb.velocity.x == 0) && !(rb.velocity.y == 0))
        {
            if (rb.velocity.x >= 4)
            {
                direction = "right";
            }
            else if (rb.velocity.x <= -4)
            {
                direction = "left";
            }
            else if (rb.velocity.y <= -4)
            {
                direction = "bottom";
            }
            else if (rb.velocity.y >= 4)
            {
                direction = "top";
            }
        }

    }

    void HandleFlipping(float angle)
    {
        Vector3 aimLocalScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
        }

        gunAimTransform.localScale = aimLocalScale;
    }

    void HandleDirectionMovement(float angle)
    {
        if (direction == "left")
        {
            gunAimTransform.transform.localScale = new Vector3(1, -1, 1);
            lastAimLocalScale = new Vector3(1, -1, 1);

            if (angle != 0)
            {
                gunAimTransform.eulerAngles = new Vector3(-180, 180, angle);
                lastAimEulerAngle = new Vector3(-180, 180, angle);
            }
            else
            {
                gunAimTransform.eulerAngles = new Vector3(-180, 180, 40);
                lastAimEulerAngle = new Vector3(-180, 180, 40);
            }

        }
        else if (direction == "right")
        {
            gunAimTransform.transform.localScale = new Vector3(1, 1, 1);
            lastAimLocalScale = new Vector3(1, 1, 1);


            if (angle != 0)
            {
                gunAimTransform.eulerAngles = new Vector3(0, 0, angle);
                lastAimEulerAngle = new Vector3(0, 0, angle);

            }
            else
            {
                gunAimTransform.eulerAngles = new Vector3(0, 0, -40);
                lastAimEulerAngle = new Vector3(0, 0, -40);
            }
        }
        else if (direction == "bottom")
        {
            gunAimTransform.transform.localScale = new Vector3(1, -1, 1);
            lastAimLocalScale = new Vector3(1, -1, 1);

            if (angle != 0)
            {
                gunAimTransform.eulerAngles = new Vector3(0, 180, angle);
                lastAimEulerAngle = new Vector3(0, 180, angle);
            }
            else
            {
                gunAimTransform.eulerAngles = new Vector3(0, 180, -90);
                lastAimEulerAngle = new Vector3(0, 180, -90);
            }

        }
        else if (direction == "top")
        {
            gunAimTransform.transform.localScale = new Vector3(1, -1, 1);
            lastAimLocalScale = new Vector3(1, -1, 1);


            if (angle != 0)
            {
                gunAimTransform.eulerAngles = new Vector3(0, 180, angle);
                lastAimEulerAngle = new Vector3(0, 180, angle);

            }
            else
            {
                gunAimTransform.eulerAngles = new Vector3(0, 0, 90);
                lastAimEulerAngle = new Vector3(0, 0, 90);
            }
            

        }
    }
}
