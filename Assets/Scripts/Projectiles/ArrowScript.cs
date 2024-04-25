using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    Rigidbody rb;
    Collider col;
    Quaternion rot = new Quaternion();

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!rb.isKinematic)
            rb.MoveRotation(Quaternion.LookRotation(rb.velocity));
        else
            transform.rotation = rot;
    }

    void OnCollisionEnter(Collision collision)
    {
        rot = rb.rotation;
        rb.isKinematic = true;
    }
}
