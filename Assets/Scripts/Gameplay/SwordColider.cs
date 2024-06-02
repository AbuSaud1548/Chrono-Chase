using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public float damageAmount = 10.0f; // Damage dealt by the sword

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CharacterHealthSystem healthSystem = other.GetComponent<CharacterHealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.DealDamage(damageAmount);
            } 
        }
    }

    // Called by the parent to enable the sword collider during attack
    public void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    // Called by the parent to disable the sword collider after attack
    public void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
}
