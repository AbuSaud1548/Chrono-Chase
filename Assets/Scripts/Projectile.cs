using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage the projectile will deal

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile hit: " + collision.gameObject.name); // Debug line

        // Check if the object we hit has a CharacterHealthSystem
        CharacterHealthSystem healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
        if (healthSystem != null)
        {
            Debug.Log("Dealing damage to: " + collision.gameObject.name); // Debug line
            // Deal damage to the object we hit
            healthSystem.DealDamage(damageAmount);
        }

        // Destroy the projectile after it hits something
        Destroy(gameObject);
    }
}
