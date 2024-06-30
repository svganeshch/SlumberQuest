using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public static spawner instance;

    public GameObject[] prefabs;
    public Transform[] spawnPoints;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (GameObject prefab in prefabs)
        {
            prefab.SetActive(false);
        }
    }

    public void SpawnPrefabs()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            foreach (Transform child in spawnPoint)
            {
                Destroy(child.gameObject);
            }
        }

        List<Transform> shuffledSpawnPoints = new List<Transform>(spawnPoints);
        ShuffleList(shuffledSpawnPoints);

        if (shuffledSpawnPoints.Count < prefabs.Length)
        {
            Debug.LogError("Not enough spawn points for the number of prefabs");
            return;
        }

        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i];
            Transform spawnPoint = shuffledSpawnPoints[i];
            GameObject spawnedPrefab = Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
            spawnedPrefab.SetActive(true);
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
        SpawnPrefabs();
    }
}
