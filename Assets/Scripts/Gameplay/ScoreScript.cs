using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance for easy access

    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component
    public GameObject panel;
    public string[] SceneBlacklist;

    private int score = 0; // Variable to store the score

    void Awake()
    {
        // Ensure there is only one instance of ScoreManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        panel.SetActive(!SceneBlacklist.Contains(SceneManager.GetActiveScene().name));
    }

    // Method to increment the score
    public void IncrementScore()
    {
        score += 1;
        UpdateScoreText();
    }

    // Method to update the score text
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}
