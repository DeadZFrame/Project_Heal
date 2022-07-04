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
    private InventorySlot _ınventorySlot;

    [System.NonSerialized]public Collider ıtem;

    private void Awake()
    {
        _ınventorySlot = FindObjectOfType<InventorySlot>();
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
                _ınventorySlot.ToText();
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

        transform.rotation = Quaternion.LookRotation(vectorInput);
    }
}
