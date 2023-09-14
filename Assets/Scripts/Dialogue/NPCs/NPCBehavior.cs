using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCBehavior : MonoBehaviour
{
    private NPCPatrolHorizontal npcPatrol;
    private bool userOnSight = false;

    private void Awake()
    {
        npcPatrol = GetComponentInParent<NPCPatrolHorizontal>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (npcPatrol != null)
        {
            npcPatrol.enabled = !userOnSight;
            if (npcPatrol.movingLeft)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            userOnSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            userOnSight = false;
        }
    }

}
