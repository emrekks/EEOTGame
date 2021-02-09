using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{

    public float distance;
    private float raycastOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PathfindingSystem()
    {
        RaycastHit _hit;
        Vector3 left = transform.position - transform.right * raycastOffset;
        Vector3 right = transform.position + transform.right * raycastOffset;

        Debug.DrawRay(left,transform.forward * distance,Color.blue);
        
        if (Physics.Raycast(left, transform.forward, out _hit,distance))
        {
            if (_hit.transform.CompareTag("Wall"))
            {
                Debug.Log("Hit");
            }
        }
        
        if (Physics.Raycast(right, transform.forward, out _hit,distance))
        {
            if (_hit.transform.CompareTag("Wall"))
            {
                Debug.Log("Hit");
            }
        }
    }
}
