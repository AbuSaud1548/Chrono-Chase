using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectile;
    PlayerControllerScript playerControllerScript;

    void Awake()
    {
        playerControllerScript = GetComponent<PlayerControllerScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
