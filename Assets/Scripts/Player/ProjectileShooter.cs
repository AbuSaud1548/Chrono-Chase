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
    public float reloadCooldown = 2.0f; // Time to reload

    private float timeCharging = 0f;
    private bool isReloading = false; // Reloading state
    private uint currentAmmo; // Current ammo count
    private FirstPersonController fpc;
    private Animator animator;

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

        // Check if we need to reload
        if (currentAmmo == 0 && !infiniteAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        // Optionally, play a reload animation or sound here
        if (animator != null)
        {
            animator.SetTrigger("Reload");
        }

        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadCooldown);

        // Refill ammo (you can set it to max or any value you prefer)
        currentAmmo = maxAmmo;

        isReloading = false;
        Debug.Log("Reload complete");
    }
}
