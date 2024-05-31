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
                Enemy.isStopped = false; // Ensure the enemy can move
                Enemy.destination = PlayerMovement.position;
                animator.SetBool("isClose", false);
            }
            else if (distanceToPlayer <= closeDistance)
            {
                if (!animator.GetBool("isClose"))
                {
                    Debug.Log("Preparing to Attack");
                    animator.SetBool("isClose", true);
                    StartCoroutine(AttackRoutine());
                }
            }
            else
            {
                Debug.Log("Player out of range");
                Enemy.isStopped = true; // Stop the enemy if out of sight range
                Enemy.destination = Enemy.transform.position;
                animator.SetBool("isClose", false);
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        isAnimating = true;
        Enemy.isStopped = true; // Stop the enemy's movement
        Enemy.velocity = Vector3.zero; // Ensure no sliding

        while (animator.GetBool("isClose"))
        {
            Debug.Log("Attacking Player");

            // Enable the sword collider at the start of the attack
            swordCollider.EnableCollider();

            // Wait for the duration of the attack animation
            yield return new WaitForSeconds(1.0f);

            float distanceToPlayer = Vector3.Distance(Enemy.transform.position, PlayerMovement.position);
            if (distanceToPlayer > closeDistance)
            {
                Debug.Log("Player out of attack range");
                animator.SetBool("isClose", false);
                isAnimating = false;
                Enemy.isStopped = false; // Resume the enemy's movement
                yield break;
            }

            // Disable the sword collider after the attack
            swordCollider.DisableCollider();

            yield return new WaitForSeconds(2.0f); // Time between attacks
        }

        isAnimating = false;
        Enemy.isStopped = false; // Resume the enemy's movement
    }

    // These methods can also be called directly from animation events if required
    public void EnableCollider()
    {
        Debug.Log("EnableCollider called");
        swordCollider.EnableCollider();
    }

    public void DisableCollider()
    {
        Debug.Log("DisableCollider called");
        swordCollider.DisableCollider();
    }
}
