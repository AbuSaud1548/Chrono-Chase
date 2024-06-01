using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The Script can be applied to all characters we want to have a health system, including player
/// </summary>
public class CharacterHealthSystem : MonoBehaviour
{
    [SerializeField]
    public float maxHealth = 100; // Stores how much is full health

    public float currentHealth { get { return health; } set { health = Mathf.Clamp(value, 0, maxHealth); } } // Character's current health
    public float invFrames { get; set; } // Stores how much time the character has left to be immune to damage

    public bool IsAlive { get { return health > 0; } } // Returns whether the character is alive or not

    private float health = 0; // private health only accessible here
    private Animator animator; // Reference to the Animator component
    public AudioClip damageSound; // Reference to the damage sound clip
    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth; // Sets the character's current health to max
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        invFrames -= Time.deltaTime; // Slowly drains invisibility frame
        invFrames = invFrames < 0 ? 0 : invFrames; // Prevents invisibility frame from being less than zero
    }

    /// <summary>
    /// Will deal damage by amount only if invisibility frames runs out (optional parameter: sets invFrames)
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="invFrames"></param>
    public void DealDamage(float amount, float invFrames = 0.2f)
    {
        if (this.invFrames <= 0 && !isDead)
        {
            currentHealth -= amount;
            this.invFrames = invFrames;

            // Play the damage sound
            if (damageSound != null)
            {
                AudioSource.PlayClipAtPoint(damageSound, transform.position);
            }

            // Check if health has dropped to 0 or below and handle death
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                // Trigger the hit animation only if not dying
                if (animator != null)
                {
                    animator.SetTrigger("Hit");
                }
            }
        }
    }

    /// <summary>
    /// Heals character by an amount
    /// </summary>
    /// <param name="amount"></param>
    public void Heal(float amount)
    {
        if (!isDead)
        {
            currentHealth += amount;
        }
    }

    private void Die()
    {
        isDead = true;

        // Trigger the death animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Disable enemy functionality
        DisableEnemy();
    }

    private void DisableEnemy()
    {
        // Disable the NavMeshAgent to stop movement
        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }

        // Optionally, disable AI scripts
        AiMovement aiMovement = GetComponent<AiMovement>();
        if (aiMovement != null)
        {
            aiMovement.enabled = false;
        }

        // Disable other components as necessary, but keep the collider enabled
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        // Freeze the Rigidbody, if it exists, to prevent falling
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
