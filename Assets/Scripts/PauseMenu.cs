using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    private bool isPaused = false;
    public GameObject PauseSound;
    private AudioSource PauseSound1;
    private void Start()
    {
        PausePanel.SetActive(false);
        AudioSource PauseSound1 = PauseSound.GetComponent<AudioSource>();
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
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Set the time scale to 0 to freeze the game
        isPaused = true;
        PausePanel.SetActive(true);
        PauseSound1.Pause();


        // Show the pause menu UI or perform any other necessary actions
        Debug.Log("Game paused");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1; // Set the time scale back to 1 to resume the game
        isPaused = false;
        PausePanel.SetActive(false);
        // Hide the pause menu UI or perform any other necessary actions
        Debug.Log("Game resumed");
        PauseSound1.Play();
    }
    public void returntoMenu()
    {
        SceneManager.LoadScene("MainMenuScene");

    }
    public void exit()
    {
        Application.Quit();

    }
}
