using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint; // This should be the transform that moves with the gun
    public AudioClip shootSfx; // Sound that will play when shooting
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
            // Instantiate a new bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            if (shootSfx != null) AudioSource.PlayClipAtPoint(shootSfx, bulletSpawnPoint.position);

            // Get the bullet's Rigidbody component
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody == null)
            {
                Debug.LogError("Bullet prefab is missing a Rigidbody component.");
                return;
            }

            // Apply forward force to the bullet
            bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;

            attackCooldownTimer = attackCooldown; // Reset attack cooldown
        }
    }
}
