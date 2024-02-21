using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{

    private HealthSystem healthSystem;
    private bool onFire = false;

    // Start is called before the first frame update
    private void Start()
    {
        healthSystem = GetComponentInParent<HealthSystem>();
    }

    private void Update()
    {
        if (onFire) healthSystem.TakeDamage((int)healthSystem.flameDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flame"))
        {
            onFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flame"))
        {
            onFire = false;
        }
    }

}
