using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    /// <summary>
    /// Camera Anchor
    /// </summary>
    public GameObject camAnchor;
    /// <summary>
    /// Mouse Sensitivity for camera rotation
    /// </summary>
    public float sensitivity = 10.0f;
    /// <summary>
    /// How far up or down can you look
    /// </summary>
    public float maxVerticalLook = 60f;
    /// <summary>
    /// How fast the player can move
    /// </summary>
    public float movementSpeed = 0.25f;

    public float jumpStrength = 1.0f;

    // Look rotation
    float v = 0f;
    float h = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetComponent<Rigidbody>().velocity += Vector3.up * jumpStrength;
        }

        Debug.Log(IsPlayerGrounded());
    }

    void FixedUpdate()
    {
        // Uses Horizontal and Vertical Axis movement to figure out which direction the player wants to move towards
        Vector3 moveV = transform.forward * movementSpeed * Input.GetAxis("Vertical");
        Vector3 moveH = transform.right * movementSpeed * Input.GetAxis("Horizontal");

        // Sets the players position to the direction the player wants to move towards
        transform.position += new Vector3(moveV.x, 0, moveV.z);
        transform.position += new Vector3(moveH.x, 0, moveH.z);

        // Uses Mouse input to calculate where the player is gonna look at
        v -= Input.GetAxis("Mouse Y") * sensitivity;
        h += Input.GetAxis("Mouse X") * sensitivity;

        // Clamps the up down camera movement to prevent camera from flipping over
        v = Mathf.Clamp(v, -maxVerticalLook, maxVerticalLook);
        
        transform.eulerAngles = new Vector3(0, h, 0); // Updates player horizontal rotation
        if (camAnchor != null) // if cam anchor is not null, update the vertical camera orbit angle
            camAnchor.transform.eulerAngles = new Vector3(v, 0, 0) + transform.eulerAngles;
    }

    /// <summary>
    /// Will check if Player is standing on the ground or not
    /// </summary>
    /// <returns></returns>
    public bool IsPlayerGrounded()
    {
        if (col != null)
        {
            Collider[] c = Physics.OverlapCapsule(col.bounds.center, col.bounds.min + new Vector3(0, col.radius * 0.985f, 0), col.radius * 0.99f);

            foreach (Collider cld in c)
                if (cld != col)
                    return true;
        }
        return false;
    }

    /// <summary>
    /// Makes the player jump
    /// </summary>
    public void Jump()
    {
        if (rb != null)
            if (IsPlayerGrounded())
                rb.velocity += Vector3.up * jumpStrength;
    }
}
