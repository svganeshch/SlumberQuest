using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planerotation : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    private Quaternion targetRotation;
    public GameObject player;
    public GameObject enemy;
    private UnityEngine.AI.NavMeshAgent enemyNavMeshAgent;
    private NavMeshSurface navMeshSurface;

    void Start()
    {
        targetRotation = transform.rotation;
        navMeshSurface = GetComponent<NavMeshSurface>();
        if (enemy != null)
        {
            enemyNavMeshAgent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
        navMeshSurface.BuildNavMesh();

        if (player != null)
        {
            player.transform.SetParent(transform);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRotation *= Quaternion.Euler(180, 0, 0);
            FlipEnemy();
            navMeshSurface.BuildNavMesh();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void FlipEnemy()
    {
        if (enemy != null && enemyNavMeshAgent != null)
        {
            Vector3 localEnemyPosition = enemy.transform.localPosition;
            Quaternion localEnemyRotation = enemy.transform.localRotation;

            localEnemyPosition.y = -localEnemyPosition.y;
            localEnemyRotation = new Quaternion(-localEnemyRotation.x, localEnemyRotation.y, localEnemyRotation.z, -localEnemyRotation.w);

            enemy.transform.localPosition = localEnemyPosition;
            enemy.transform.localRotation = localEnemyRotation;

            enemyNavMeshAgent.Warp(enemy.transform.position);
        }
    }
}
