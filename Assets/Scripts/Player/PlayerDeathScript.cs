using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeathScript : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject retryBtn;
    public GameObject mainmenuBtn;

    CharacterHealthSystem healthSystem;
    bool playerDied = false;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<CharacterHealthSystem>();
        retryBtn = GameObject.Find("Retry Button");
        mainmenuBtn = GameObject.Find("Main Menu Button");

        retryBtn.GetComponent<Button>().onClick.AddListener(RetryLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem != null)
        {
            if (!healthSystem.IsAlive && !playerDied)
            {
                playerDied = true;
                GetComponent<FirstPersonController>().enabled = false;
                Time.timeScale = 0;
                if (deathScreen != null) deathScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

    }

    void RetryLevel()
    {
        Debug.Log("Retry");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
