using System;
using System.Collections;
using System.Security.Cryptography;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody _rb;

    private CameraBase _cameraBase;
    private InventoryManager _ınventoryManager;
    private RigidbodyManager _rigidbodyManager;

    [System.NonSerialized]public Collider ıtem;

    private bool grounded = false;

    private void Awake()
    {
        _rigidbodyManager = GameObject.Find("TABLE_BREAKABLE").GetComponent<RigidbodyManager>();
        _rb = GetComponent<Rigidbody>();
        _cameraBase = GameObject.Find("Main Camera").GetComponent<CameraBase>();
        _ınventoryManager = GameObject.Find("InventoryUI").GetComponent<InventoryManager>();
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
                grounded = true;
            }
        }
        else
        {
            if (collision.transform.tag.Equals("Ground"))
            {
                var colID = collision.gameObject.name;
                _cameraBase.floor = GameObject.Find(colID).GetComponent<Transform>();
                grounded = true;
            }
        }

        if (collision.transform.tag.Equals("EnvObjects"))
        {
            foreach (var obj in _rigidbodyManager.objects)
            {
                obj.constraints = RigidbodyConstraints.None;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Item"))
        {
            if (other.gameObject.GetComponent<ItemWorld>() == null)
            {
                ıtem = other;
                ıtem.gameObject.AddComponent<ItemWorld>();
            }
            else
            {
                Destroy(other.gameObject.GetComponent<ItemWorld>());
                _ınventoryManager.AddItem();
                InventorySlot.slot.ToText();
                other.gameObject.SetActive(false);
            }
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

        Jump();
        transform.rotation = Quaternion.LookRotation(vectorInput);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            _rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            grounded = false;
        }
    }
}
