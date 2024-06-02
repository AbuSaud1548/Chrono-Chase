using System;
using System.Collections;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public bool requiresChargeUp = false;
    public bool doesChargeAffectFOV = true;
    public float chargeUpTime = 1f;
    public bool infiniteAmmo = true;
    public uint maxAmmo = 16; // Max ammo capacity
    public float shootPower = 16;
    public GameObject cam;
    public AudioClip shootSound;
    public float damageAmount = 10f; // Add damage amount
    public float reloadTime = 1f; // Time to reload
    public AudioClip reloadSound;
    public bool isAutomatic;
    public float automaticTime = 0.1f;
    public uint currentAmmo { get; private set; } // Current ammo count

    [NonSerialized]
    public bool isReloading = false; // Reloading state

    private float timeCharging = 0f;
    private FirstPersonController fpc;
    private Animator animator;
    private float reloadTimer = 0f;
    private float automaticTimer = 0f;

    void Start()
    {
        fpc = GetComponent<FirstPersonController>();
        animator = GetComponent<Animator>(); // Assuming the Animator is on the same GameObject
        currentAmmo = maxAmmo; // Initialize current ammo
    }

    void Update()
    {
        if (cam != null && projectilePrefab != null && !isReloading)
        {
            if ((!requiresChargeUp && (isAutomatic ? Input.GetKey(KeyCode.Mouse0) && automaticTimer <= 0 : Input.GetKeyDown(KeyCode.Mouse0))) || (requiresChargeUp && timeCharging >= chargeUpTime && Input.GetKeyUp(KeyCode.Mouse0)))
            {
                if (isAutomatic) automaticTimer = automaticTime;
                Shoot();
            }
            if (requiresChargeUp)
            {
                if (Input.GetKey(KeyCode.Mouse0)) timeCharging += Time.deltaTime;

                fpc.canSprint = !Input.GetKey(KeyCode.Mouse0);
                fpc.zoomFOV = Mathf.Lerp(fpc.fov, 60, timeCharging / chargeUpTime);
            }
        }

        if (isAutomatic)
        {
            automaticTimer -= Time.deltaTime;
            automaticTimer = automaticTimer < 0 ? 0 : automaticTimer;
        }

        // You can only reload if ammo is infinite
        if (!infiniteAmmo)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo) // Reload button that will start the reloading process
            {
                isReloading = true;
                reloadTimer = 0;

                if (reloadSound != null) AudioSource.PlayClipAtPoint(reloadSound, transform.position); // Plays reload sound
            }

            if (isReloading) reloadTimer += Time.deltaTime; // Reload timer

            if (reloadTimer >= reloadTime) // Will refill ammo when reload timer reaches the reload time
            {
                isReloading = false;
                currentAmmo = maxAmmo;
                reloadTimer = 0;
            }
        }
    }

    public void Shoot()
    {
        if (isReloading || (currentAmmo == 0 && !infiniteAmmo))
        {
            // If reloading or out of ammo, do nothing
            return;
        }

        timeCharging = 0f;
        if (!infiniteAmmo) currentAmmo--;

        if (shootSound != null) AudioSource.PlayClipAtPoint(shootSound, transform.position);

        Transform camTransform = cam.transform;
        Vector3 spawnPosition = camTransform.position + (camTransform.forward * 1.5f); // Spawn bullet slightly in front of the player
        GameObject instProjectile = Instantiate(projectilePrefab, spawnPosition, camTransform.rotation);
        instProjectile.layer = LayerMask.NameToLayer("Projectile"); // Set the projectile's layer

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
