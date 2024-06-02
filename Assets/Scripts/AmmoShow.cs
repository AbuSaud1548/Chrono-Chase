using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoShow : MonoBehaviour
{
    public ProjectileShooter shooter;
    TextMeshProUGUI t;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shooter != null && t != null)
        {
            t.text = shooter.currentAmmo + " / " + shooter.maxAmmo;
        }
        else { gameObject.SetActive(false); }
    }
}
