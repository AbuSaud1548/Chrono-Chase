using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint; // This should be the transform that moves with the gun
    public float bulletSpeed = 50f; // Adjust as necessary
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
            Debug.Log("Shooting a bullet"); // Debug log

            // Ensure the bullet spawns at the current position and rotation of the bulletSpawnPoint
            Vector3 spawnPosition = bulletSpawnPoint.position;
            Quaternion spawnRotation = bulletSpawnPoint.rotation;

            // Instantiate a new bullet
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            Debug.Log("Bullet instantiated at " + spawnPosition); // Debug log

            // Get the bullet's Rigidbody component
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody == null)
            {
                Debug.LogError("Bullet prefab is missing a Rigidbody component.");
                return;
            }

            // Apply forward force to the bullet
            bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
            Debug.Log("Bullet velocity set"); // Debug log

            attackCooldownTimer = attackCooldown; // Reset attack cooldown
        }
    }
}
