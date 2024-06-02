using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0) transform.localPosition = Input.GetKey(KeyCode.Mouse0) ? new Vector3(0.75f, 0.6f, -0.1f) : Vector3.zero;
    }
}
