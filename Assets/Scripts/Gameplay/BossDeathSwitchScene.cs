using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeathSwitchScene : MonoBehaviour
{
    GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            GameObject.Find("Score Manager").transform.parent = transform;
            SceneManager.LoadScene("FinalScene"); // When Boss despawns or does not exists, game will transition to the end scene
        }
    }
}
