using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    Transform PlayerMovement;
    NavMeshAgent Enemy;
    public Animator animator;
    public float closeDistance = 3.0f;
    public float sightRange = 40.0f;
    public float attackCooldown = 2.0f; // Time between attacks
    private bool isAnimating = false;
    private SwordCollider swordCollider;

    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
        PlayerMovement = GameObject.Find("FirstPersonController").transform;
        animator = GetComponent<Animator>();
        swordCollider = GetComponentInChildren<SwordCollider>(); // Find the SwordCollider in children
        swordCollider.DisableCollider(); // Ensure the collider starts disabled
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(Enemy.transform.position, PlayerMovement.position);

        if (!isAnimating)
        {
            if (distanceToPlayer < sightRange && distanceToPlayer > closeDistance)
            {
                Debug.Log("Chasing Player");
                ResumeMovement();
                Enemy.destination = PlayerMovement.position;
                animator.SetBool("isWalking", true); // Trigger walking animation
                animator.SetBool("isAttacking", false); // Ensure attack animation is not playing
            }
            else if (distanceToPlayer <= closeDistance)
            {
                if (!animator.GetBool("isAttacking"))
                {
                    Debug.Log("Preparing to Attack");
                    StartCoroutine(AttackRoutine());
                }
            }
            else
            {
                Debug.Log("Player out of range");
                StopMovement(); // Stop the enemy if out of sight range
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        isAnimating = true;
        StopMovement(); // Stop the enemy's movement

        animator.SetBool("isAttacking", true);
        animator.SetBool("isWalking", false);

        // Enable the sword collider at the start of the attack
        swordCollider.EnableCollider();

        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(1.0f);

        // Disable the sword collider after the attack
        swordCollider.DisableCollider();

        // Wait for cooldown period before allowing another attack
        yield return new WaitForSeconds(attackCooldown);

        isAnimating = false;

        float distanceToPlayer = Vector3.Distance(Enemy.transform.position, PlayerMovement.position);
        if (distanceToPlayer <= closeDistance)
        {
            Debug.Log("Re-attacking Player");
            StartCoroutine(AttackRoutine());
        }
        else
        {
            Debug.Log("Player out of attack range");
            animator.SetBool("isAttacking", false);
            ResumeMovement(); // Resume the enemy's movement
        }
    }

    void StopMovement()
    {
        Enemy.isStopped = true;
        Enemy.velocity = Vector3.zero; // Ensure no sliding
        animator.SetBool("isWalking", false); // Ensure walking animation stops
    }

    void ResumeMovement()
    {
        Enemy.isStopped = false;
        animator.SetBool("isWalking", true); // Resume walking animation
    }

    // These methods can also be called directly from animation events if required
    public void EnableCollider()
    {
        swordCollider.EnableCollider();
    }

    public void DisableCollider()
    {
        swordCollider.DisableCollider();
    }
}
