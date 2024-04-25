using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public bool infiniteAmmo = true;
    public uint ammo = 16;
    public float shootPower = 16;
    PlayerControllerScript playerControllerScript;

    void Awake()
    {
        playerControllerScript = gameObject.GetComponent<PlayerControllerScript>();
    }

    void Update()
    {
        if (playerControllerScript != null && projectilePrefab != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                if (ammo > 0 || infiniteAmmo)
                {
                    if (!infiniteAmmo) ammo--;
                    Transform camAnchorTransform = playerControllerScript.camAnchor.transform;
                    GameObject instProjectile = Instantiate(projectilePrefab, camAnchorTransform.position + (camAnchorTransform.forward * gameObject.GetComponent<CapsuleCollider>().radius), new Quaternion());
                    Rigidbody rb;
                    if (instProjectile.TryGetComponent<Rigidbody>(out rb))
                        rb.velocity = camAnchorTransform.forward * shootPower;
                }
            }
        }
    }
}
