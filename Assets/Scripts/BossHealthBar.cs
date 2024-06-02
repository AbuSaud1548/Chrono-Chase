using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthBarGUI;
    public CharacterHealthSystem healthSystem;

    // Update is called once per frame
    void Update()
    {
        healthBarGUI.value = healthSystem.currentHealth / healthSystem.maxHealth;
    }
}
