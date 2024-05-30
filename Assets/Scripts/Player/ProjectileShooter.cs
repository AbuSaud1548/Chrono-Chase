using System.Collections;
using System.Collections.Generic;
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

    private float timeCharging = 0f;
    private FirstPersonController fpc;

    void Start()
    {
        fpc = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        if (cam != null && projectilePrefab != null)
        {
            if ((!requiresChargeUp && Input.GetKeyDown(KeyCode.Mouse0)) || (requiresChargeUp && timeCharging >= chargeUpTime && Input.GetKeyUp(KeyCode.Mouse0)))
            {
                if (ammo > 0 || infiniteAmmo) // Checks if player has ammo, ignores check if infiniteAmmo is true
                {
                    timeCharging = 0f; // Resets charge to zero
                    if (!infiniteAmmo) ammo--; // consumes ammo if infiniteAmmo is false

                    // Plays the shooting sound only if shootSound is not null
                    if (shootSound != null) AudioSource.PlayClipAtPoint(shootSound, transform.position);

                    // Spawns projectile at player camera position
                    Transform camTransform = cam.transform;
                    GameObject instProjectile = Instantiate(projectilePrefab, camTransform.position + (camTransform.forward * gameObject.GetComponent<CapsuleCollider>().radius), new Quaternion());
                    Rigidbody rb;
                    if (instProjectile.TryGetComponent<Rigidbody>(out rb)) // if projectile has rigid body, it will cause the projectile to shoot and move in camera's direction
                        rb.velocity = camTransform.forward * shootPower;
                }
            }
            if (requiresChargeUp) // Script below will only occur if projectile shooter requires the player to charge up before shooting
            {
                if (Input.GetKey(KeyCode.Mouse0)) timeCharging += Time.deltaTime; // Charges up the projectile shooter

                fpc.canSprint = !Input.GetKey(KeyCode.Mouse0); // Disables sprinting when charging up shooter
                fpc.zoomFOV = Mathf.Lerp(fpc.fov, 60, timeCharging / chargeUpTime); // Changes FOV based on the current shooter charge
            }
        }
    }
}
