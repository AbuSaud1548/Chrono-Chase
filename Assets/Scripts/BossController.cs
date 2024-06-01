using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float timeBetweenShots = 0.5f;
    public int shotsPerBurst = 5;
    public float burstInterval = 30f;

    private Animator animator;
    private bool isShooting = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("ShootBurst", burstInterval, burstInterval);
    }

    private void ShootBurst()
    {
        if (!isShooting)
        {
            
            isShooting = true;
            animator.SetBool("Shoot", true);

            for (int i = 0; i < shotsPerBurst; i++)
            {
                Invoke("Shoot", i * timeBetweenShots);
                
            }

            Invoke("ResetShooting", shotsPerBurst * timeBetweenShots);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    private void ResetShooting()
    {
        isShooting = false;
        animator.SetBool("Shoot", false);
    }
}
