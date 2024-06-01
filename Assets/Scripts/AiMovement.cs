using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    Transform PlayerMovement;
    NavMeshAgent Enemy;
    public Animator animator;
    public float closeDistance = 1f;
    public float sightRange = 40.0f;
    public float attackCooldown = 2.0f; // Time between attacks
    private bool isAnimating = false;
    private SwordCollider swordCollider;
    private ProjectileShooter projectileShooter;
    public bool isRangedEnemy = false; // Flag to differentiate between melee and ranged enemies

    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
        PlayerMovement = GameObject.Find("FirstPersonController").transform;
        animator = GetComponent<Animator>();

        if (isRangedEnemy)
        {
            projectileShooter = GetComponentInChildren<ProjectileShooter>();
            if (projectileShooter == null)
            {
                Debug.LogError("ProjectileShooter component not found on ranged enemy or its children.");
            }
        }
        else
        {
            swordCollider = GetComponentInChildren<SwordCollider>();
            if (swordCollider == null)
            {
                Debug.LogError("SwordCollider component not found on melee enemy or its children.");
            }
            else
            {
                swordCollider.DisableCollider(); // Ensure the collider starts disabled
            }
        }
    }

    void Update()
    {
        if (!isAnimating)
        {
            EvaluateDistanceToPlayer();
        }
    }

    void EvaluateDistanceToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(Enemy.transform.position, PlayerMovement.position);

        if (distanceToPlayer < sightRange && distanceToPlayer > closeDistance)
        {
            ResumeMovement();
            Enemy.destination = PlayerMovement.position;
            animator.SetBool("isWalking", true); // Trigger walking animation
            animator.SetBool("isAttacking", false); // Ensure attack animation is not playing
        }
        else if (distanceToPlayer <= closeDistance)
        {
            if (!animator.GetBool("isAttacking"))
            {
                StartCoroutine(AttackRoutine());
            }
        }
        else
        {
            StopMovement(); // Stop the enemy if out of sight range
        }
    }

    IEnumerator AttackRoutine()
    {
        isAnimating = true;
        StopMovement(); // Stop the enemy's movement

        animator.SetBool("isAttacking", true);
        animator.SetBool("isWalking", false);

        if (isRangedEnemy)
        {
            if (projectileShooter != null)
            {
                projectileShooter.Shoot();
            }
            else
            {
                Debug.LogError("ProjectileShooter component is null. Cannot shoot.");
            }
        }
        else
        {
            if (swordCollider != null)
            {
                swordCollider.EnableCollider();
                yield return new WaitForSeconds(1.0f);
                swordCollider.DisableCollider();
            }
            else
            {
                Debug.LogError("SwordCollider component is null. Cannot attack.");
            }
        }

        yield return new WaitForSeconds(attackCooldown);

        isAnimating = false;

        EvaluateDistanceToPlayer();
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

    public void EnableCollider()
    {
        if (!isRangedEnemy && swordCollider != null)
        {
            swordCollider.EnableCollider();
        }
    }

    public void DisableCollider()
    {
        if (!isRangedEnemy && swordCollider != null)
        {
            swordCollider.DisableCollider();
        }
    }
}
