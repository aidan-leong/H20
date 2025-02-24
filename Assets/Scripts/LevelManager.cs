using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // GameObject to activate when the game is paused
    [SerializeField] private GameObject pauseMenu;

    // Tracks whether the game is paused
    private bool isPaused = false;

    private void Update()
    {
        // Check for the Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (isPaused)
            {
                LoadHome();
            }
        }
    }

    // Pause the game
    public void PauseGame()
    {
        if (isPaused) return; // Prevent duplicate pausing
        Time.timeScale = 0; // Freeze the game
        isPaused = true;

        // Activate the pause menu
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Pause menu GameObject is not assigned in the Inspector!");
        }
    }

    // Resume the game
    public void ResumeGame()
    {
        if (!isPaused) return; // Prevent resuming if not paused
        Time.timeScale = 1; // Resume the game
        isPaused = false;

        // Deactivate the pause menu
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
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

    public void LoadHome()
    {
        Time.timeScale = 1; // Ensure game is running before changing scenes
        SceneManager.LoadScene("MainMenu");
    }
}
