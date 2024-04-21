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
    /// Player Camera
    /// </summary>
    public Camera cam;
    /// <summary>
    /// Mouse Sensitivity for camera rotation
    /// </summary>
    public float sensitivity = 10.0f;
    /// <summary>
    /// How far up or down can you look
    /// </summary>
    public float maxVerticalLook = 60f;
    /// <summary>
    /// How fast the player can move around
    /// </summary>
    public float movementSpeed = 0.25f;
    /// <summary>
    /// How fast the player sprints
    /// </summary>
    public float sprintingSpeed = 0.5f;
    /// <summary>
    /// How strong the jump is
    /// </summary>
    public float jumpStrength = 1.0f;
    /// <summary>
    /// How easily you can move midair
    /// </summary>
    public float airControl = 0.1f;

    private Animator animator;


    // Look rotation
    float v = 0f;
    float h = 0f;

    Rigidbody rb;
    CapsuleCollider col;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        // Gets the rigid body and collider of the player
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        // Locks the mouse in the center of the screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Uses Mouse input to calculate where the player is gonna look at
        v -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        h += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        // Clamps the up down camera movement to prevent camera from flipping over
        v = Mathf.Clamp(v, -maxVerticalLook, maxVerticalLook);

        transform.eulerAngles = new Vector3(0, h, 0); // Updates player horizontal rotation
        if (camAnchor != null) // if cam anchor is not null, update the vertical camera orbit angle
            camAnchor.transform.eulerAngles = new Vector3(v, 0, 0) + transform.eulerAngles;

        if (camAnchor != null)
        {
            float camDist = 5; // Max camera distance
            RaycastHit hit;
            Physics.Raycast(camAnchor.transform.position, -camAnchor.transform.forward, out hit, camDist); // does a raycast for camera collision check
            if (cam != null) // Adjusts camera so that it doesn't clip into objects and scenes
                cam.transform.localPosition = new Vector3(0, 0, -(hit.collider != null ? hit.distance - 0.3f : camDist));
        }

        // Left clicking will lock the mouse and hide it
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        // Pressing escape will free mouse and make it visible
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Player jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            animator.SetBool("IsJumping", true);
        }
        else {
            if(IsPlayerGrounded()){
            animator.SetBool("IsJumping", false);
            }

        }


        if (IsPlayerMoving())
        {
            // Set animator parameter for movement to true
            animator.SetBool("isMoving", true);
        }
        else
        {
            // Set animator parameter for movement to false
            animator.SetBool("isMoving", false);
        }

         if (IsPlayerSprinting())
        {
            // Set animator parameter for movement to true
            animator.SetBool("IsRunning", true);
        }
        else
        {
            // Set animator parameter for movement to false
            animator.SetBool("IsRunning", false);
        }

        Debug.Log(IsPlayerSprinting());

    }

    void FixedUpdate()
    {
        // Uses Horizontal and Vertical Axis movement to figure out which direction the player wants to move towards
        Vector3 moveV = transform.forward * (IsPlayerSprinting() ? sprintingSpeed : movementSpeed) * Input.GetAxis("Vertical") * (IsPlayerGrounded() ? 1 : airControl);
        Vector3 moveH = transform.right * (IsPlayerSprinting() ? sprintingSpeed : movementSpeed) * Input.GetAxis("Horizontal") * (IsPlayerGrounded() ? 1 : airControl);

        // Sets the players position to the direction the player wants to move towards
        rb.velocity += new Vector3(moveV.x, 0, moveV.z);
        rb.velocity += new Vector3(moveH.x, 0, moveH.z);

        // Player grounded friction
        rb.velocity *= IsPlayerGrounded() ? 0.8f : 0.99f;
    }

    /// <summary>
    /// Will check if Player is standing on the ground or not
    /// </summary>
    /// <returns></returns>
    public bool IsPlayerGrounded()
    {
        if (col != null)
        {
            // Gets all colliders overlapping the Physics.OverlapCapsule bounds
            Collider[] c = Physics.OverlapCapsule(col.bounds.center, col.bounds.min + new Vector3(0, col.radius * 0.985f, 0), col.radius * 0.99f);

            // Will return true if it finds a collider that is not the player's collider
            foreach (Collider cld in c)
                if (cld != col)
                    return true;
        }
        return false; // Returns false if it didn't find any other collider
    }

    public bool IsPlayerMoving()
    {
        return Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;
    }

    /// <summary>
    /// Makes the player jump
    /// </summary>
    public void Jump()
    {
        if (rb != null)
            if (IsPlayerGrounded()) // Will check if the player is grounded before applying the jump
                rb.velocity += Vector3.up * jumpStrength;
    }

    /// <summary>
    /// Will return true when Player is pressing the sprint button
    /// </summary>
    /// <returns></returns>
    public bool IsPlayerSprinting() 
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}
