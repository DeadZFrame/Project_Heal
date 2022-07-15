using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

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
        //DontDestroyOnLoad(this.gameObject);
        ınventory = new Inventory();
        _playerBase = gameObject.GetComponent<PlayerBase>();
        if(GameObject.FindGameObjectWithTag("EnvObjects") != null)
            _rigidbodyManager = GameObject.FindGameObjectWithTag("EnvObjects").GetComponent<RigidbodyManager>();
        _cameraBase = GameObject.Find("Main Camera").GetComponent<CameraBase>();
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _uıManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _timer = _uıManager.GetComponent<Timer>();
    }

    private void Start()
    {
        ınventoryUI.SetInventory(ınventory);
        _materialList = new List<Material>();

        repairBar = _playerBase.repairBar.GetComponent<RepairBar>();
    }

    private void Update()
    {
        if (_timer.timeIsRunning && repairBar.repaired)
        {
            _levelManager.starsForThisLevel += 1;
            starsManager.ChangeSprite();
            _timer.timeIsRunning = false;
        }
    }

    private void FixedUpdate()
    {
        XRay();
    }

    [NonSerialized] public RepairBar repairBar;
    public StarsManager starsManager;
    private UIManager _uıManager;

    public Transform starTransform;

    private Timer _timer;

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
                    switch (colID.name)
                    {
                        case "Bottom" when _timer.time > 0 && !repairBar.repaired:
                            _timer.timeIsRunning = true;
                            break;
                        case "Outdoor" when repairBar.repaired:
                        {
                            if(_timer.time >= _timer.time/2)
                                _levelManager.starsForThisLevel += 1;
                            repairBar.repaired = false;
                            repairBar.repairBar.value = 0f;
                            starsManager.ChangeSprite();
                            _levelManager.level = (int)LevelManager.SceneIndex.Level01;
                            LevelManager.Save("Level", _levelManager.level);
                            var star = starsManager.stars[0].transform.parent.gameObject;
                            star.transform.parent = starTransform.parent;
                            //UnityEditor.GameObjectUtility.SetParentAndAlign(star, starTransform.gameObject);
                            star.GetComponent<RectTransform>().position = starTransform.GetComponent<RectTransform>().position + new Vector3(-70, 50);
                            _uıManager.missionComplete.SetActive(true);
                            break;
                        }
                    }
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
                    _playerBase.grounded = true;
                    foreach (var obj in _rigidbodyManager.objects)
                    {
                        obj.constraints = RigidbodyConstraints.None;
                        obj.gameObject.layer = LayerMask.NameToLayer("HitObj");
                        /*var objCollider = obj.GetComponent<BoxCollider>();
                        if (objCollider.bounds
                            .Intersects(gameObject.GetComponent<BoxCollider>().bounds))
                        {
                            objCollider.isTrigger = true;
                        }*/
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
        switch (other.tag)
        {
            case "Star":
                _levelManager.starsForThisLevel += 1;
                _levelManager.ManageStars();
                other.gameObject.SetActive(false);
                starsManager.ChangeSprite();
                break;

            case "Item":
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
                break;
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
