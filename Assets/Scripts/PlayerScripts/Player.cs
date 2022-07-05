using System;
using System.Collections;
using System.Security.Cryptography;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CameraBase _cameraBase;
    private RigidbodyManager _rigidbodyManager;
    private PlayerBase _playerBase;
    [SerializeField]private InventoryUI ınventoryUI;

    private Inventory _ınventory;

    private void Awake()
    {
        _playerBase = gameObject.GetComponent<PlayerBase>();
        _rigidbodyManager = GameObject.FindGameObjectWithTag("EnvObjects").GetComponent<RigidbodyManager>();
        _cameraBase = GameObject.Find("Main Camera").GetComponent<CameraBase>();
        
        _ınventory = new Inventory();
        ınventoryUI.SetInventory(_ınventory);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent != null)
        {
            if (collision.transform.parent.tag.Equals("Ground") || collision.transform.tag.Equals("Ground"))    
            {
                var colID = collision.gameObject.name;
                _cameraBase.floor = GameObject.Find(colID).GetComponent<Transform>();
                _playerBase.grounded = true;
            }
        }
        else
        {
            if (collision.transform.tag.Equals("Ground"))
            {
                var colID = collision.gameObject.name;
                _cameraBase.floor = GameObject.Find(colID).GetComponent<Transform>();
                _playerBase.grounded = true;
            }
        }

        if (collision.transform.parent.tag.Equals("EnvObjects"))
        {
            foreach (var obj in _rigidbodyManager.objects)
            {
                obj.constraints = RigidbodyConstraints.None;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Item"))
        {
            ItemWorld ıtemWorld = other.GetComponent<ItemWorld>();
            if (ıtemWorld != null)
            {
                _ınventory.AddItem(ıtemWorld.GetItem());
                ıtemWorld.DestroySelf();
            }
        }
    }
}
