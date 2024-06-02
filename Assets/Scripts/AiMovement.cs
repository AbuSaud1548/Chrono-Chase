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
    private EnemyShooter enemyShooter;
    public bool isRangedEnemy = false; // Flag to differentiate between melee and ranged enemies

    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
        PlayerMovement = GameObject.Find("FirstPersonController").transform;
        animator = GetComponent<Animator>();

        if (isRangedEnemy)
        {
            enemyShooter = GetComponentInChildren<EnemyShooter>();
            if (enemyShooter == null)
            {
                Debug.LogError("EnemyShooter component not found on ranged enemy or its children.");
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

        // Adjust NavMeshAgent settings if necessary
        Enemy.speed = 3.5f;
        Enemy.acceleration = 8.0f;
        Enemy.angularSpeed = 120.0f;
        Enemy.baseOffset = 0.5f; // Adjust based on your character's height

        // Ensure Animator root motion is disabled
        animator.applyRootMotion = false;
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
        Debug.Log("Distance to player: " + distanceToPlayer); // Debug log

        if (distanceToPlayer < sightRange && distanceToPlayer > closeDistance)
        {
            ResumeMovement();
            Enemy.destination = PlayerMovement.position;
            Debug.Log("Setting destination to player position: " + PlayerMovement.position); // Debug log
            animator.SetBool("isWalking", true); // Trigger walking animation
        }
        else if (distanceToPlayer <= closeDistance)
        {
            if (!isAnimating)
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

        animator.SetBool("isWalking", false);
        animator.SetTrigger("isAttacking"); // Set attack trigger

        // Wait for 0.1 seconds before shooting
        yield return new WaitForSeconds(0.1f);

        if (isRangedEnemy)
        {
            if (enemyShooter != null)
            {
                Debug.Log("Calling Shoot method on enemyShooter"); // Debug log
                enemyShooter.Shoot();
            }
            else
            {
                Debug.LogError("EnemyShooter component is null. Cannot shoot.");
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
