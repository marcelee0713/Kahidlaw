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
        // Y axis or pa vertical pre na position
        movement.x = joystick.Horizontal * moveSpeed;
        // X axis or pa horizontal pre na position
        movement.y = joystick.Vertical * moveSpeed;
        // 
        Vector2 speed = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed).normalized;

        // Ito yung sineset niya yung velocity niya. Which is ang speed ng isang object
        // speed ng "X axis" multiply by movespeed
        // speed ng "Y axis" multiply by movespeed.
        rb.velocity = new Vector2(speed.x * moveSpeed, speed.y * moveSpeed);

        // Animation ito pre. Tuturuan na lang kita bukas para dito
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }
}
