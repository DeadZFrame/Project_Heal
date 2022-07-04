using System;
using UnityEditor.AssetImporters;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody _rb;

    private CameraBase _cameraBase;
    
    public Collider item;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraBase = GameObject.Find("Main Camera").GetComponent<CameraBase>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent != null)
        {
            if (collision.transform.parent.tag.Equals("Ground") || collision.transform.tag.Equals("Ground"))    
            {
                var colID = collision.gameObject.name;
                _cameraBase.floor = GameObject.Find(colID).GetComponent<Transform>();
            }
        }
        else
        {
            if (collision.transform.tag.Equals("Ground"))
            {
                var colID = collision.gameObject.name;
                _cameraBase.floor = GameObject.Find(colID).GetComponent<Transform>();
            }
        }

        if (collision.transform.tag.Equals("Item"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Item"))
        {
            item = other;
        }
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

        transform.rotation = Quaternion.LookRotation(vectorInput);
    }
}
