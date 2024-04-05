using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public GameObject camAnchor;
    public float sensitivity = 10.0f;
    public float maxVerticalLook = 60f;
    public float movementSpeed = 0.25f;

    // Look rotation
    float v = 0f;
    float h = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveV = transform.forward * movementSpeed * Input.GetAxis("Vertical");
        Vector3 moveH = transform.right * movementSpeed * Input.GetAxis("Horizontal");

        transform.position += new Vector3(moveV.x, 0, moveV.z);
        transform.position += new Vector3(moveH.x, 0, moveH.z);

        // Uses Mouse input to calculate where the player is gonna look at
        v -= Input.GetAxis("Mouse Y") * sensitivity;
        h += Input.GetAxis("Mouse X") * sensitivity;

        // Clamps the up down camera movement to prevent camera from flipping over
        v = Mathf.Clamp(v, -maxVerticalLook, maxVerticalLook);
        
        transform.eulerAngles = new Vector3(0, h, 0);
        if (camAnchor != null)
            camAnchor.transform.eulerAngles = new Vector3(v, 0, 0) + transform.eulerAngles;
    }

    public bool IsPlayerGrounded()
    {
        // TODO : Figure out a way to detect whether the player is standing on the ground or not
        return true;
    }
}
