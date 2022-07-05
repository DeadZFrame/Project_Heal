using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerBase : MonoBehaviour
{
    private Rigidbody _rb;

    public float moveSpeed;
    public float jumpForce;
    [NonSerialized]public bool grounded = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 currentPos = _rb.transform.position;

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");


        Vector3 vectorInput = new Vector3(horizontalInput, 0, verticalInput);
        vectorInput = Vector3.ClampMagnitude(vectorInput, 1);

        Vector3 movement = vectorInput * moveSpeed;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;

        _rb.MovePosition(newPos);

        Jump();
        transform.rotation = Quaternion.LookRotation(vectorInput);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        }
    }
}
