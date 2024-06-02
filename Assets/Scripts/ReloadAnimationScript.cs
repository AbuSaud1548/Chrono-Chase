using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAnimationScript : MonoBehaviour
{
    public ProjectileShooter shooter;
    public Vector3 reloadPos;

    // Update is called once per frame
    void Update()
    {
        if (shooter != null)
        {
            transform.localPosition = shooter.isReloading ? reloadPos : Vector3.zero;
        }
    }
}
