using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
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
