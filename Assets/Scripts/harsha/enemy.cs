using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetDestinationToPlayer();
    }

    void Update()
    {
        SetDestinationToPlayer();
    }

    void SetDestinationToPlayer()
    {
        if (player != null && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
