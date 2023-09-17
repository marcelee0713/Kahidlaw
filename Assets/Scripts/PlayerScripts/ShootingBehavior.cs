using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootingBehavior : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 1f;

    private Vector2 aimDirection;
    public Joystick gunJoyStick;

    private void Update()
    {
        aimDirection = gunJoyStick.Direction.normalized;
    }
}
