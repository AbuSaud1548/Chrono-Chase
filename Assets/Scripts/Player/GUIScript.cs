using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour
{
    public GameObject healthBarGUI;

    Slider healthBarSlider;
    CharacterHealthSystem healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = healthBarGUI.GetComponent<Slider>();
        healthSystem = GetComponent<CharacterHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSlider.value = healthSystem.currentHealth / healthSystem.maxHealth;
    }
}
