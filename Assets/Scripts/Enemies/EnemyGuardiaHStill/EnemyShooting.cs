using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShooting : MonoBehaviour
{
    [Header("Muzzles")]
    public bool isInShootingRange = false;
    public Transform muzzleLocation;
    public GameObject bullet;
    public float bulletSpeed;

    GameObject projectile;
    [Header("Horizontal Detectors")]
    [SerializeField] private LeftDetector leftDetector;
    [SerializeField] private RightDetector rightDetector;
    [SerializeField] private HorizontalOnRanged OnRangedH;
    [SerializeField] private FourDirectionOnRanged OnRanged4D;

    [Header("Vertical Detectors")]
    [SerializeField] private FrontDetector FDetector;
    [SerializeField] private BackDetector BDetector;

    public Transform playerLocation;

    public void EnemyShoot()
    {
        //Destroy(projectile);
        projectile = Instantiate(bullet, muzzleLocation.position, Quaternion.identity);
        if(OnRanged4D == null)
        {
            if (OnRangedH.inRangedEnemy)
            {
                Vector2 moveDirection = (playerLocation.transform.position - projectile.transform.position).normalized * bulletSpeed;
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
                projectile.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(0, 0) * Mathf.Rad2Deg);
            }
        }
        else
        {
            if (OnRanged4D.inRangedOn4d)
            {
                Vector2 moveDirection = (OnRanged4D.currentTargetPos.transform.position - projectile.transform.position).normalized * bulletSpeed;
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
                projectile.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(0, 0) * Mathf.Rad2Deg);
            }
        }
    }
}
