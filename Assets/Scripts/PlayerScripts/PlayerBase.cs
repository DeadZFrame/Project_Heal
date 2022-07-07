using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerBase : MonoBehaviour
{
    private Player _player;
    
    private Rigidbody _rb;

    private Transform[] _childs;
    private ItemWorld _obj;
    private Item _ıtem;

    private Vector3 _direction;
    
    public float moveSpeed;
    public float jumpForce;
    [NonSerialized]public bool grounded = false;

    private void Awake()
    {
        _player = gameObject.GetComponent<Player>();
        _childs = gameObject.GetComponentsInChildren<Transform>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
        Interact();
    }

    private void Movement()
    {
        var currentPos = _rb.transform.position;

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");


        var vectorInput = new Vector3(horizontalInput, 0, verticalInput);
        vectorInput = Vector3.ClampMagnitude(vectorInput, 1);

        var movement = vectorInput * moveSpeed;
        var newPos = currentPos + movement * Time.fixedDeltaTime;

        _rb.MovePosition(newPos);
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(ray, out var distance))
        {
            var target = ray.GetPoint(distance);
            _direction = target - transform.position;
            var lookPos = Mathf.Atan2(_direction.x, _direction.z)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, lookPos, 0);
        }

        Jump();
    }

    private void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !grounded) return;
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        grounded = false;
    }

    private void Swing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Swing Animation
        }
    }

    private void Interact()
    {
        foreach (var getKey in ItemAssets.Instance.keyData)
        {
            if (!Input.GetKeyDown(getKey) || _player.ınventory.GetItemList().Count <= 0) continue;
            
            foreach (var ıtem in _player.ınventory.GetItemList())
            {
                if (ıtem.keyCode != getKey) continue;
                
                foreach (var child in _childs)
                {
                    if (!child.name.Equals("Hand")) continue;
                    
                    _ıtem = ıtem;
                    
                    if (!_player.ınventory.toggled)
                    {
                        _obj = ItemWorld.SpawnItemWorld(child.position, new Item { ıtemTypes = ıtem.ıtemTypes});
                        _obj.transform.parent = child.transform;
                        _obj.tag = "InventoryItem";
                        _player.ınventory.ToggleItem();
                    }
                    else
                    {
                        _obj.DestroySelf();
                        _player.ınventory.ToggleItem();
                    }
                }
            }
        }
        
        if(_player.ınventory.toggled) _obj.transform.localPosition = new Vector3(0, 0, 0);

        if (!Input.GetKeyDown(KeyCode.Q)) return;
        if (!_player.ınventory.toggled) return;
        
        _obj.transform.parent = null;
        _obj.tag = "Item";
        _obj.GetComponent<BoxCollider>().isTrigger = false;
        _obj.GetComponent<Rigidbody>().useGravity = true;
        _obj.GetComponent<Rigidbody>().AddForce(_direction * jumpForce/2, ForceMode.Impulse);
                
        _player.ınventory.RemoveItem(_ıtem);
        _player.ınventory.ToggleItem();
    }
}
