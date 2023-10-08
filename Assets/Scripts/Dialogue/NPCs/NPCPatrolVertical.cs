using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrolVertical : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform topEdge;
    [SerializeField] private Transform backEdge;

    [Header("NPC")]
    [SerializeField] private Transform npc;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    public bool movingDown;
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
        if (anim != null) anim.SetBool("isMoving", false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (movingDown)
        {
            if (npc.position.y >= backEdge.position.y)
                MoveInDirection(-1, 0);
            else
                DirectionChange();
        }
        else
        {
            if (npc.position.y <= topEdge.position.y)
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
                movingDown = !movingDown;
        }

    }

    public void MoveInDirection(int _direction, int timer)
    {
        if (anim != null)
        {
            idleTimer = timer;
            anim.SetBool("isMoving", true);

            // Face the direciton
            anim.SetFloat("Vertical", _direction);

            // Move to that direction
            npc.position = new Vector3(npc.position.x,
                npc.position.y + Time.deltaTime * _direction * speed, npc.position.z);
        }


    }
}
