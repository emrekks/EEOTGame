using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DevilEnemy : MonoBehaviour
{

    private NavMeshAgent _agent;

    public GameObject player;
    
    
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        _agent.SetDestination(player.transform.position);
    }
    
}
