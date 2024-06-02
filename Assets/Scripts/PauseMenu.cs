using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    private bool isPaused = false;
    private void Start()
    {
        PausePanel.SetActive(false);
    }
    private void Update()
    {
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

        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Return)) returntoMenu();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Set the time scale to 0 to freeze the game
        isPaused = true;
        PausePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Set the time scale back to 1 to resume the game
        isPaused = false;
        PausePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void returntoMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
