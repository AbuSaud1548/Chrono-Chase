using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Instantiate a new bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the bullet's Rigidbody component
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Apply forward force to the bullet
        bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
}
