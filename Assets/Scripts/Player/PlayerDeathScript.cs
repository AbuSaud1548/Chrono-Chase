using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeathScript : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject deathTextBG;
    public GameObject deathText;
    public float fadeTime = 3f;

    CharacterHealthSystem healthSystem;
    Image deathBG;
    Image deathBG2;
    TextMeshProUGUI dTxt;
    bool playerDied = false;
    float timeSinceDeath = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<CharacterHealthSystem>();

        deathBG = deathScreen.GetComponent<Image>();
        deathBG2 = deathTextBG.GetComponent<Image>();
        dTxt = deathText.GetComponent<TextMeshProUGUI>();

        if (deathBG != null)
        {
            var c = deathBG.color;
            c.a = 0;
            deathBG.color = c;
        }
        if (deathBG2 != null)
        {
            var c = deathBG2.color;
            c.a = 0;
            deathBG2.color = c;
        }
        if (dTxt != null)
        {
            var c = dTxt.color;
            c.a = 0;
            dTxt.color = c;
        }
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
                GetComponent<ProjectileShooter>().enabled = false;
                GetComponent<PlayerKeyTracker>().enabled = false;
                if (deathScreen != null) deathScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (!healthSystem.IsAlive)
            {
                timeSinceDeath += Time.deltaTime;

                if (deathBG != null)
                {
                    var c = deathBG.color;
                    c.a = Mathf.Lerp(0, 0.5f, timeSinceDeath / fadeTime);
                    deathBG.color = c;
                }
                if (deathBG2 != null)
                {
                    var c = deathBG2.color;
                    c.a = Mathf.Lerp(0, 0.75f, timeSinceDeath / fadeTime);
                    deathBG2.color = c;
                }
                if (dTxt != null)
                {
                    var c = dTxt.color;
                    c.a = Mathf.Lerp(0, 1f, timeSinceDeath / fadeTime);
                    dTxt.color = c;
                }

                if (timeSinceDeath > 5)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
