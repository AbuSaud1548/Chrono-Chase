using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public float damageAmount = 10.0f; // Damage dealt by the sword

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sword hit " + other.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CharacterHealthSystem healthSystem = other.GetComponent<CharacterHealthSystem>();
            if (healthSystem != null)
            {
                Debug.Log("Dealing damage to " + other.name);
                healthSystem.DealDamage(damageAmount);
            } 
        }
    }

    // Called by the parent to enable the sword collider during attack
    public void EnableCollider()
    {
        Debug.Log("Sword Collider Enabled");
        GetComponent<Collider>().enabled = true;
    }

    // Called by the parent to disable the sword collider after attack
    public void DisableCollider()
    {
        Debug.Log("Sword Collider Disabled");
        GetComponent<Collider>().enabled = false;
    }
}
