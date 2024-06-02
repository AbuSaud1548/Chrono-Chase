using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint; // This should be the transform that moves with the gun
    public AudioClip shootSfx; // Sound that will play when shooting
    public float bulletSpeed = 50f; // Adjust as necessary
    public float attackCooldown = 2.0f; // Time between attacks
    private float attackCooldownTimer = 0.0f;
    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        // Find the player object
        playerTransform = GameObject.Find("FirstPersonController").transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player object not found. Please ensure the player object is named 'FirstPersonController'.");
        }
    }

    private void Update()
    {
        attackCooldownTimer -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (attackCooldownTimer <= 0 && playerTransform != null)
        {
            // Calculate direction to the player's center
            Vector3 directionToPlayer = (playerTransform.position - bulletSpawnPoint.position).normalized;

            // Instantiate a new bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(directionToPlayer));

            if (shootSfx != null) AudioSource.PlayClipAtPoint(shootSfx, bulletSpawnPoint.position);

            // Get the bullet's Rigidbody component
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody == null)
            {
                Debug.LogError("Bullet prefab is missing a Rigidbody component.");
                return;
            }

            // Apply velocity towards the player's center
            bulletRigidbody.velocity = directionToPlayer * bulletSpeed;

            attackCooldownTimer = attackCooldown; // Reset attack cooldown
        }
    }
}
