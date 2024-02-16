using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    private SwordTraining parent;

    private void Start()
    {
        parent = GetComponentInParent<SwordTraining>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            parent.haveBeenHit = true;
            Destroy(this.gameObject);
        }
    }
}
