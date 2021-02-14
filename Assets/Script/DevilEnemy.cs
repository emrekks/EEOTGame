using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DevilEnemy : MonoBehaviour
{

    private NavMeshAgent _agent; 
    [SerializeField]private float[] distance;
    [SerializeField]private GameObject _target;

    public float range;

    [SerializeField]private bool playerSelected;



    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        NearestPlayer();

        if (playerSelected)
        {
            _agent.SetDestination(_target.transform.position);
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

                if (thisDistance < minDistance)
                {
                    minDistance = thisDistance;
                    _target = players[i];
                    playerSelected = true;
                }
                
            }
            
        }
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
