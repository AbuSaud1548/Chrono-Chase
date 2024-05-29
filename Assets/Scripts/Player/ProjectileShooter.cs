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
                if (ammo > 0 || infiniteAmmo)
                {
                    timeCharging = 0f;
                    if (!infiniteAmmo) ammo--;
                    Transform camAnchorTransform = cam.transform;
                    GameObject instProjectile = Instantiate(projectilePrefab, camAnchorTransform.position + (camAnchorTransform.forward * gameObject.GetComponent<CapsuleCollider>().radius), new Quaternion());
                    Rigidbody rb;
                    if (instProjectile.TryGetComponent<Rigidbody>(out rb))
                        rb.velocity = camAnchorTransform.forward * shootPower;
                }
            }
            if (requiresChargeUp)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    timeCharging += Time.deltaTime;
                }

                fpc.canSprint = !Input.GetKey(KeyCode.Mouse0);
                fpc.zoomFOV = Mathf.Lerp(fpc.fov, 60, timeCharging / chargeUpTime);

                Debug.Log(timeCharging);

            }
        }
    }
}
