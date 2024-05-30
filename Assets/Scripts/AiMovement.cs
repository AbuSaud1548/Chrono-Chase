using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    Transform PlayerMovement;
    NavMeshAgent Enemy;
    public Animator animator;  
    public float closeDistance = 5.0f;  
    private bool isClose = false;
    private bool isAnimating = false;

    
    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
        PlayerMovement = GameObject.Find("FirstPersonController").transform;
        animator = GetComponent<Animator>();  
    }

   
    void Update()
    {
        if (!isAnimating)
        {
            Enemy.destination = PlayerMovement.position;

            
            float distanceToPlayer = Vector3.Distance(Enemy.transform.position, PlayerMovement.position);

            
            if (distanceToPlayer < closeDistance)
            {
                if (!isClose)
                {
                    isClose = true;
                    animator.SetBool("isClose", true);  
                    StartCoroutine(ResetIsCloseAfterAnimation());  
                }
            }
        }
    }

    IEnumerator ResetIsCloseAfterAnimation()
    {
        isAnimating = true;

       
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        isClose = false;
        animator.SetBool("isClose", false);  

        isAnimating = false;
    }
}
