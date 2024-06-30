using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerinputs : MonoBehaviour
{
    public spawner spawners; // Reference to the Spawner script
    private List<int> playerPattern = new List<int>();

    void Update()
    {
        // Example input handling (replace with your own input logic)
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerPattern.Add(0);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerPattern.Add(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerPattern.Add(2);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerPattern.Add(3);
        }

        // Example: Call CheckPattern when player presses Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Implement your pattern checking logic here
            bool isPatternCorrect = CheckPattern(playerPattern);
            if (!isPatternCorrect)
            {
                spawners.RegenerateAndRespawn();
            }
            playerPattern.Clear(); // Clear the player's input for the next pattern
        }
    }

    private bool CheckPattern(List<int> playerPattern)
    {
        // Implement your pattern checking logic here
        // This is just a placeholder for your actual pattern checking
        return false;
    }
}
