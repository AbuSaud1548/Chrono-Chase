using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene1Transition : MonoBehaviour
{
    public float transitionTime = 10f; // Time in seconds for cut scene to finish
    public string nextLevelName;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= transitionTime)
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
    
}

