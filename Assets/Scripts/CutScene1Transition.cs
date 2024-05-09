using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene1Transition : MonoBehaviour
{
        public float transitionTime = 10f; // Time in seconds for cut scene to finish

        private float timer = 0f;

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= transitionTime)
            {
                LoadNextScene();
            }
        }

        private void LoadNextScene()
        {
        // Perform the scene transition
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // check active scene and then make transition to next scene in the build
    }
    }

