using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainPush = 1000;
    [SerializeField] float rotationPush = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPush();
        ProcessRotation();
    }

    void ProcessPush()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // To make the force(push) applied frame rate independent, need to mult by: Time.deltaTime
            rb.AddRelativeForce(Vector3.up * mainPush * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationPush);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationPush);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }

}
