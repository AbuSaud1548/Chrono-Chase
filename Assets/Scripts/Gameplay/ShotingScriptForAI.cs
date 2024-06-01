using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float attackCooldown = 2.0f; // Time between attacks
    private float attackCooldownTimer = 0.0f;

    private void Update()
    {
        attackCooldownTimer -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (attackCooldownTimer <= 0)
        {
            // Instantiate a new bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Get the bullet's Rigidbody component
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            // Apply forward force to the bullet
            bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;

            attackCooldownTimer = attackCooldown; // Reset attack cooldown
        }
    }
}
