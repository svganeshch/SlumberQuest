using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius;
    public int spawnCount;
    public float circleSpawnSpacing = 1f;
    public bool circleSpawn = false;

    public GameObject[] spawnPrefabs;
    public List<Enemy> spawnedObjs = new List<Enemy>();

    public int patrolPointsCount = 4;
    private List<Vector3> patrolPoints = new List<Vector3>();

    private void Start()
    {
        if (circleSpawn)
        {
            SpawnCircle();
        }
        else
        {
            //Spawn();
        }
    }

    private void AssignPatrolPoints()
    {
        patrolPoints.Clear();

        for (int i = 0; i < patrolPointsCount; i++)
        {
            Vector3 randomPatrolPoint = Random.insideUnitCircle * spawnRadius;
            randomPatrolPoint = new Vector3(transform.position.x + randomPatrolPoint.x, transform.position.y, transform.position.z + randomPatrolPoint.y);

            patrolPoints.Add(randomPatrolPoint);
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            AssignPatrolPoints();

            Vector3 spawnPosition = patrolPoints[0];
            GameObject prefabToSpawn = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
            GameObject spawnedObj = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            spawnedObjs.Add(spawnedObj.GetComponent<Enemy>());
        }
    }

    private void SpawnCircle()
    {
        // Calculate the angle increment
        float angleIncrement = Mathf.PI * 2 / spawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            // Calculate the angle for this spawn position
            float angle = i * angleIncrement;

            // Calculate the position on the circumference of the circle
            Vector3 spawnPosition = new Vector3(Mathf.Cos(angle) * spawnRadius, 0, Mathf.Sin(angle) * spawnRadius) + transform.position;

            // Select a random prefab to spawn
            GameObject prefabToSpawn = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];

            // Instantiate the prefab at the calculated position
            GameObject spawnedObj = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            spawnedObj.transform.LookAt(transform.position);

            // Add the spawned object to the list
            spawnedObjs.Add(spawnedObj.GetComponent<Enemy>());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
