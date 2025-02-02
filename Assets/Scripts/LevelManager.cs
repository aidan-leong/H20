using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // List of GameObjects to remain active even during pause
    [SerializeField] private GameObject[] unaffectedElements;

    // Tracks whether the game is paused
    private bool isPaused = false;

    // Pause the game
    public void PauseGame()
    {
        if (isPaused) return; // Prevent duplicate pausing
        Time.timeScale = 0; // Freeze the game
        isPaused = true;

        // Keep unaffected elements active
        foreach (var element in unaffectedElements)
        {
            if (element != null)
                element.SetActive(true);
        }
    }

    // Resume the game
    public void ResumeGame()
    {
        if (!isPaused) return; // Prevent resuming if not paused
        Time.timeScale = 1; // Resume the game
        isPaused = false;
    }

    // Restart the current scene
    public void RestartLevel()
    {
        Time.timeScale = 1; // Ensure game is running before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Load a specific scene by build index
    public void ChangeScene(int buildIndex)
    {
        Time.timeScale = 1; // Ensure game is running before changing scenes
        SceneManager.LoadScene(buildIndex);
    }
}
