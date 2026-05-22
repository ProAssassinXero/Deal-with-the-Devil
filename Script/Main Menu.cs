using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start the game by loading a scene
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Game Quit");

        Application.Quit();
    }
}
