using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitScript : MonoBehaviour
{
    public float healAmount = 30;

    void OnTriggerEnter(Collider other)
    {
        CharacterHealthSystem hs = other.gameObject.GetComponent<CharacterHealthSystem>();
        if (hs != null) // If CharacterHealthSystem Script exists, it will try to heal
        {
            // Medkit will only be picked up if health is not full
            if (hs.currentHealth < hs.maxHealth)
            {
                // will be healed and medkit destroyed
                hs.Heal(healAmount);
                Destroy(transform.gameObject);
            }
        }
    }
}
