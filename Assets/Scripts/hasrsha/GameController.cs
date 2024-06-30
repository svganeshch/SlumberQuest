using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public spawner spawners; // Reference to the Spawner script

    void Update()
    {
        // Example: Call SpawnPrefabs when player presses the Play button (space key in this case)
        if (Input.GetKeyDown(KeyCode.X))
        {
            spawners.SpawnPrefabs();
        }
    }
}
