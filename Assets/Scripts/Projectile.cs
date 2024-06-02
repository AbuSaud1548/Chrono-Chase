using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    public float damageAmount; // Amount of damage the projectile will deal

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object we hit has a CharacterHealthSystem
        CharacterHealthSystem healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
        if (healthSystem != null)
        {
            // Deal damage to the object we hit
            healthSystem.DealDamage(damageAmount);
        }

        // Destroy the projectile after it hits something
        Destroy(gameObject);
    }
}
