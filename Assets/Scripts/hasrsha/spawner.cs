using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject[] prefabs; // Assign your 4 prefabs here
    public Transform[] spawnPoints; // Assign the spawn points in the inspector

    private void Awake()
    {
        // Disable all prefabs initially
        foreach (GameObject prefab in prefabs)
        {
            prefab.SetActive(false);
        }
    }

    public void SpawnPrefabs()
    {
        // Clear any existing prefabs at the spawn points (optional)
        foreach (Transform spawnPoint in spawnPoints)
        {
            foreach (Transform child in spawnPoint)
            {
                Destroy(child.gameObject);
            }
        }

        // Create a list of spawn points and shuffle it
        List<Transform> shuffledSpawnPoints = new List<Transform>(spawnPoints);
        ShuffleList(shuffledSpawnPoints);

        // Ensure we have at least as many spawn points as prefabs
        if (shuffledSpawnPoints.Count < prefabs.Length)
        {
            Debug.LogError("Not enough spawn points for the number of prefabs");
            return;
        }

        // Spawn each prefab at a shuffled spawn point
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i];
            Transform spawnPoint = shuffledSpawnPoints[i];
            GameObject spawnedPrefab = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            spawnedPrefab.SetActive(true); // Activate the spawned prefab
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void RegenerateAndRespawn()
    {
        // Call this method to regenerate the pattern and respawn the prefabs
        SpawnPrefabs();
    }
}
