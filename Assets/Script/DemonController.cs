using System.Collections;
using System.Collections.Generic;
using Mirror.Examples.NetworkRoom;
using UnityEngine;

public class DemonController : MonoBehaviour
{

    private EnemyController _enemyController;

    [SerializeField]private bool ritualStarted = false;

    private int playerNumber;
    
    
    void Start()
    {
        _enemyController = GetComponent<EnemyController>();
        var players = GameObject.FindGameObjectsWithTag("Player");
        playerNumber = players.Length;
    }

    
    void Update()
    {
        if (ritualStarted)
        {
            int rnd = Random.Range(0, playerNumber);
            

            // for (int i = 0; i < playerNumber; i++)
            // {
            //     
            // }
        }
    }
}
