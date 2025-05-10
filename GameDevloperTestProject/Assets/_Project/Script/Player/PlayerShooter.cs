using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float cooldown = 0.5f;

    private float nextFireTime;
    
    void Update()
    {
        if (InventoryManager.Instance.equipmentSlot == null)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + cooldown;
        }
    }

    void Fire()
    {
        Vector2 direction = transform.localScale.x < 0 ? Vector2.left : Vector2.right;

        GameObject projectileObj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        ProjectileBullet projectile = projectileObj.GetComponent<ProjectileBullet>();

        int damage = Random.Range(3, 6);
        projectile.Init(direction, damage);
    }
}
