using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Public variable to specify the scene name in the Inspector
    [SerializeField] private string sceneToLoad;

    // Method to load the specified scene
    public void PlayGame()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log($"Play Button Clicked! Loading scene: {sceneToLoad}");
            SceneManager.LoadScene(sceneToLoad); // Load the scene specified in the Inspector
        }
        else
        {
            Debug.LogWarning("Play Button Clicked, but no scene specified in 'sceneToLoad'!");
        }
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked! Exiting the game...");
        Application.Quit();

        // Note: Application.Quit() only works in a built game.
        // In the editor, the game will not exit, so use Debug.Log to confirm it's working.
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void SkillTree()
    {
        SceneManager.LoadScene("SkillTree");
    }
}
