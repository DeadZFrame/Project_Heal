using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class PlayerBase : MonoBehaviour
{
    private Player _player;
    private ItemWorld _obj;
    private Item _ıtem;
    
    private Rigidbody _rb;
    
    public Transform hand;
    public Hammer hammer;

    private Animator _animator;

    private Vector3 _direction, _vectorInput;

    public float moveSpeed, jumpForce;
    [NonSerialized]public bool grounded = false;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _player = gameObject.GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_player.ınventory.toggled)
        {
            hammer.transform.parent.gameObject.SetActive(false);
            Throw();
        }
        else
        {
            hammer.transform.parent.gameObject.SetActive(true);
            Swing();
        }
        Movement();
        CycleTroughInventory();
        Jump();
    }

    private void Movement()
    {
        if (grounded)
        {
            moveSpeed = 8;
            _animator.SetBool("Grounded", true);
        }
        var currentPos = _rb.transform.position;
            
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");


        _vectorInput = new Vector3(horizontalInput, 0, verticalInput);
        _vectorInput = Vector3.ClampMagnitude(_vectorInput, 1);

        var movement = _vectorInput * moveSpeed;
        var newPos = currentPos + movement * Time.fixedDeltaTime;

        _rb.MovePosition(newPos);
        
        if (horizontalInput != 0 || verticalInput != 0)
        {
            _animator.SetBool("IsWalking", true);
            _animator.SetBool("IsIdle", false);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsIdle", true);
        }

        if (_animator.GetBool("IsIdle") || _animator.GetBool("IsAttack"))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(ray, out var distance))
            {
                var target = ray.GetPoint(distance);
                _direction = target - transform.position;
                var lookPos = Mathf.Atan2(_direction.x, _direction.z)*Mathf.Rad2Deg;
                var rot = Quaternion.Euler(0, lookPos, 0);
                transform.rotation = rot;
                //Quaternion.Lerp(transform.rotation, rot, .125f);
            }
        }
        else
        {
            if (grounded)
            {
                var rot = Quaternion.LookRotation(_vectorInput);
                transform.rotation = rot;
            }
        }
    }

    private void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !grounded) return;
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        moveSpeed = 4;

        if (_vectorInput.x != 0 || _vectorInput.z != 0)
        {
            moveSpeed = 2;
            var pos =  hand.position - transform.position;
            _rb.AddForce(pos * 1.5f, ForceMode.Impulse);
        }

        _animator.SetTrigger("Jump");

        grounded = false;
        _animator.SetBool("Grounded", false);
    }

    private void Swing()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _animator.SetTrigger("Attack");
            _animator.SetBool("IsAttack", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) StartCoroutine(DisableAttackAnimationBool());

        if (!_animator.GetBool("IsAttack")) return;
        
        var objects = Physics.OverlapSphere(hammer.transform.position, hammer.attackRange, hammer.objLayer);
        foreach (var obj in objects)
        {
            obj.GetComponent<Rigidbody>().AddForce(obj.transform.position - transform.position * hammer.force, ForceMode.Impulse);
        }
    }

    private void Throw()
    {
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

    private void CycleTroughInventory()
    {
        foreach (var getKey in ItemAssets.Instance.keyData)
        {
            if (!Input.GetKeyDown(getKey) || _player.ınventory.GetItemList().Count == 0) continue;
            
            foreach (var ıtem in _player.ınventory.GetItemList())
            {
                if (ıtem.keyCode != getKey) continue;

                _ıtem = ıtem;
                
                if (!_player.ınventory.toggled)
                {
                    _obj = ItemWorld.SpawnItemWorld(Vector3.zero, new Item { ıtemTypes = ıtem.ıtemTypes});
                    _obj.transform.parent = hand.transform;
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
        
        if(_player.ınventory.toggled) _obj.transform.localPosition = new Vector3(0, 0, 0);
    }

    private IEnumerator DisableAttackAnimationBool()
    {
        yield return new WaitForSeconds(.4f);
        _animator.SetBool("IsAttack", false);
    }
}
