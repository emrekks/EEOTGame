using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    private NavMeshAgent _agent; 
    [SerializeField]private float[] distance;
    [SerializeField]private GameObject _target;

    public float range;

    [SerializeField]private bool playerSelected;


    NavMeshHit _navMeshHit;
    private NavMeshPath _path;

    public float wanderRadius;
    public bool isWandering;
    

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    
    void FixedUpdate()
    {
        NearestPlayer();
        FaceTarget();


        if (playerSelected)
        {
            _agent.SetDestination(_target.transform.position);
        }
        else
        {
            WanderPoint();
            
            if (Vector3.Distance(transform.position, _agent.destination) <= 2f)
            {
                isWandering = false;
            }
        }
        
    }

    void NearestPlayer()
    {
        float minDistance = range;
        var players= GameObject.FindGameObjectsWithTag("Player");
        
        foreach (var player in players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                float thisDistance = Mathf.Abs(Vector3.Distance(players[i].transform.position, transform.position));

                distance[i] = thisDistance;
                
                
                if (_agent.Raycast(players[i].transform.position, out _navMeshHit))
                {
                    continue;
                }

                if (thisDistance < minDistance)
                {
                    minDistance = thisDistance;
                    _target = players[i];
                    playerSelected = true;
                }
                
            }
            
        }

    }

    void WanderPoint()
    {
        if (!isWandering)
        {
            Vector3 rnd = Random.insideUnitSphere * wanderRadius;
            _agent.SetDestination(rnd);
            isWandering = true;
        }
        
    }
    
    void FaceTarget()
    {
        if (playerSelected)
        {
            Vector3 direction = (_agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,range);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,wanderRadius);
    }
}
