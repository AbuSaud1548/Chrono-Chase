using System.Collections;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public bool requiresChargeUp = false;
    public bool doesChargeAffectFOV = true;
    public float chargeUpTime = 1f;
    public bool infiniteAmmo = true;
    public uint ammo = 16;
    public float shootPower = 16;
    public GameObject cam;
    public AudioClip shootSound;
    public float damageAmount = 10f; // Add damage amount

    private float timeCharging = 0f;
    private FirstPersonController fpc;
    private Animator animator;

    void Start()
    {
        fpc = GetComponent<FirstPersonController>();
        animator = GetComponent<Animator>(); // Assuming the Animator is on the same GameObject
    }

    void Update()
    {
        if (cam != null && projectilePrefab != null)
        {
            if ((!requiresChargeUp && Input.GetKeyDown(KeyCode.Mouse0)) || (requiresChargeUp && timeCharging >= chargeUpTime && Input.GetKeyUp(KeyCode.Mouse0)))
            {
                Shoot();
            }
            if (requiresChargeUp)
            {
                if (Input.GetKey(KeyCode.Mouse0)) timeCharging += Time.deltaTime;

                fpc.canSprint = !Input.GetKey(KeyCode.Mouse0);
                fpc.zoomFOV = Mathf.Lerp(fpc.fov, 60, timeCharging / chargeUpTime);
            }
        }
    }

    public void Shoot()
    {
        if (ammo > 0 || infiniteAmmo)
        {
            timeCharging = 0f;
            if (!infiniteAmmo) ammo--;

            if (shootSound != null) AudioSource.PlayClipAtPoint(shootSound, transform.position);

            Transform camTransform = cam.transform;
            Vector3 spawnPosition = camTransform.position + (camTransform.forward * 1.5f); // Spawn bullet slightly in front of the player
            GameObject instProjectile = Instantiate(projectilePrefab, spawnPosition, camTransform.rotation);
            instProjectile.layer = 1 << LayerMask.NameToLayer("PlayerProjectile"); // Set the projectile's layer

            Rigidbody rb;
            if (instProjectile.TryGetComponent<Rigidbody>(out rb))
            {
                rb.velocity = camTransform.forward * shootPower;

                Projectile projectileScript = instProjectile.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.damageAmount = damageAmount;
                }
            }

            // Trigger the shooting animation
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }
        }
    }
}
