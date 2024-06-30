using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Space"); // Replace with your game scene name
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
