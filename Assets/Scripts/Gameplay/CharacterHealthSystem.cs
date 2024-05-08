using UnityEngine;

/// <summary>
/// The Script can be applied to all characters we want to have a health system, including player
/// </summary>
public class CharacterHealthSystem : MonoBehaviour
{
    [SerializeField]
    public float maxHealth = 100; // Stores how much is full health

    public float currentHealth { get { return health; } set { health = Mathf.Clamp(value, 0, maxHealth); } } // Character's current health
    public float invFrames { get; set; } // Stores how much time the character has left to be immune to damage


    private float health = 0; // private health only accessable here


    void Awake()
    {
        currentHealth = maxHealth; // Sets the character's current health to max
    }

    void Update()
    {
        invFrames -= Time.deltaTime; // Slowly drains invisibility frame
        invFrames = invFrames < 0 ? 0 : invFrames; // Prevents invisibility frame to be less than zero
    }

    /// <summary>
    /// Will deal damage by amount only if invisibility frames runs out (optional parameter: sets invFrames)
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="invFrames"></param>
    public void DealDamage(float amount, float invFrames = 0.2f)
    {
        if (this.invFrames <= 0)
        {
            currentHealth -= amount;
            this.invFrames = invFrames;
        }
    }

    /// <summary>
    /// Heals character by an amount
    /// </summary>
    /// <param name="amount"></param>
    public void Heal(float amount)
    {
        currentHealth += amount;
    }
}
