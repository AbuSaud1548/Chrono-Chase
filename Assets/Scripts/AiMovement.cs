using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    public Transform PlayerMovement;
    NavMeshAgent Enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Enemy.destination = PlayerMovement.position;
        
    }
}
