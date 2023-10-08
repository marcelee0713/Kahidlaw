using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrolHorizontal : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("NPC")]
    [SerializeField] private Transform npc;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    public bool movingLeft;
    [SerializeField]
    private Rigidbody2D npcRb;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("NPC Animator")]
    [SerializeField] private Animator anim;


    private void Awake()
    {
        initScale = npc.localScale;
    }

    private void OnDisable()
    {
        if (anim != null)
        {
            if (movingLeft)
                anim.SetFloat("Horizontal", -1);
            else
                anim.SetFloat("Horizontal", 1);

            anim.SetBool("isMoving", false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (movingLeft)
        {
            if (npc.position.x >= leftEdge.position.x)
                MoveInDirection(-1, 0);
            else
                DirectionChange();
        }
        else
        {
            if (npc.position.x <= rightEdge.position.x)
                MoveInDirection(1, 0);
            else
                DirectionChange();
        }
    }


    public void DirectionChange()
    {
        if (anim != null)
        {
            anim.SetBool("isMoving", false);
            idleTimer += Time.deltaTime;

            if (idleTimer > idleDuration)
                movingLeft = !movingLeft;
        }

    }

    public void MoveInDirection(int _direction, int timer)
    {
        if (anim != null)
        {
            idleTimer = timer;
            anim.SetBool("isMoving", true);

            // Face the direciton
            anim.SetFloat("Horizontal", _direction);

            // Move to that direction
            npc.position = new Vector3(npc.position.x + Time.deltaTime * _direction * speed,
                npc.position.y, npc.position.z);
        }


    }
}
