using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private CameraBase _cameraBase;
    private RigidbodyManager _rigidbodyManager;
    private PlayerBase _playerBase;
    private LevelManager _levelManager;

    [SerializeField]private InventoryUI ınventoryUI;

    public Inventory ınventory;
    
    private void Awake()
    {
        ınventory = new Inventory();
        _playerBase = gameObject.GetComponent<PlayerBase>();
        if(GameObject.FindGameObjectWithTag("EnvObjects") != null)
            _rigidbodyManager = GameObject.FindGameObjectWithTag("EnvObjects").GetComponent<RigidbodyManager>();
        _cameraBase = GameObject.Find("Main Camera").GetComponent<CameraBase>();
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Start()
    {
        ınventoryUI.SetInventory(ınventory);
        _materialList = new List<Material>();
    }

    private void FixedUpdate()
    {
        XRay();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent != null)
        {
            switch (collision.transform.parent.tag)
            {
                case "Ground":
                {
                    _playerBase.grounded = true;
                    var colID = collision.transform;
                    _cameraBase.floor = colID;
                    break;
                }
                /*case "EnvObjects":
                {
                    var broke = false;
                    rigidbodyManager.objects = collision.transform.GetComponentsInChildren<Rigidbody>();
                    foreach (var obj in rigidbodyManager.objects)
                    {
                        if (obj.constraints == RigidbodyConstraints.FreezeAll)
                        {
                            if (!broke)
                            {
                                var parent = collision.transform.GetComponentInParent<Transform>().position;
                                ItemWorld.SpawnItemWorld(new Vector3(parent.x, parent.y, 1.5f), new Item{ıtemTypes = Item.ItemTypes.Plank});   
                            }

                            broke = true;
                        }
                        obj.constraints = RigidbodyConstraints.None;
                        var objCollider = obj.GetComponent<BoxCollider>();
                        if (objCollider.bounds
                            .Intersects(gameObject.GetComponent<BoxCollider>().bounds))
                        {
                            objCollider.isTrigger = true;
                        }
                    }
                    rigidbodyManager.Initialize();
                    break;
                }*/
                case "Wall":
                    _rigidbodyManager.objects = collision.transform.GetComponents<Rigidbody>();
                    foreach (var obj in _rigidbodyManager.objects)
                    {
                        obj.constraints = RigidbodyConstraints.None;
                        var objCollider = obj.GetComponent<BoxCollider>();
                        if (objCollider.bounds
                            .Intersects(gameObject.GetComponent<BoxCollider>().bounds))
                        {
                            objCollider.isTrigger = true;
                        }
                    }
                    _rigidbodyManager.Initialize();
                    break;
            }
        }
        else
        {
            switch (collision.transform.tag)
            {
                case "Ground":
                {
                    _playerBase.grounded = true;
                    if(collision.transform.parent == null) return;
                    var colID = collision.transform.parent;
                    _cameraBase.floor = colID;
                    break;
                }
                case "Item":
                    if(!collision.transform.GetComponent<ItemWorld>().canBeTaken) return;
                    collision.collider.isTrigger = true;
                    collision.transform.GetComponent<Rigidbody>().useGravity = false;
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Star"))
        {
            _levelManager.starsForThisLevel += 1;
        }
        if (!other.tag.Equals("Item")) return;
        
        var ıtemWorld = other.GetComponent<ItemWorld>();
        var ıtemMagnet = other.gameObject.GetComponent<ItemMagnet>();

        if(ınventory.GetItemList().Count > 5) return;
        
        if (ıtemMagnet.enabled)
        {
            if (ıtemWorld == null) return;
            ınventory.AddItem(ıtemWorld.GetItem());
            ıtemWorld.DestroySelf();
        }
        else
        {
            ıtemMagnet.enabled = true;
        }
    }
    
    private RaycastHit _hitInfo;
    public LayerMask roof;

    public Material transparentMaterial, opaqueMaterial;
    private List<Material> _materialList;

    private bool _onHit =  false;

    private void XRay()
    {
        var camPos = Camera.main.transform.position;
        var direction = transform.position - camPos;
        
        
        var hit = Physics.Raycast(camPos, direction, out _hitInfo, 1000, roof);
        if (hit)
        {
            if(_onHit) return;
            var materials = _hitInfo.collider.gameObject.GetComponent<MeshRenderer>().materials;
            
            foreach (var material in materials)
            {
                material.shader = transparentMaterial.shader;
                _materialList.Add(material);
            }

            for (var i = 0; i < materials.Length; i++)
            {
                materials[i] = null;
            }

            _onHit = true;
        }
        else
        {
            if(_materialList == null) return;
            foreach (var material in _materialList.ToList())
            {
                material.shader = opaqueMaterial.shader;
                _materialList.Remove(material);
            }

            _onHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, Camera.main.transform.position);
    }
}
