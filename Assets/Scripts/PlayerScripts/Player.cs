using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private CameraBase _cameraBase;
    private RigidbodyManager _rigidbodyManager;
    private PlayerBase _playerBase;

    [SerializeField]private InventoryUI ınventoryUI;

    public Inventory ınventory;

    private bool _exists = false;

    private void Awake()
    {
        _playerBase = gameObject.GetComponent<PlayerBase>();
        _rigidbodyManager = GameObject.FindGameObjectWithTag("EnvObjects").GetComponent<RigidbodyManager>();
        _cameraBase = GameObject.Find("Main Camera").GetComponent<CameraBase>();
    }

    private void Start()
    {
        ınventory = new Inventory();
        ınventoryUI.SetInventory(ınventory);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent != null)
        {
            switch (collision.transform.parent.tag)
            {
                case "Ground":
                {
                    var colID = collision.gameObject.name;
                    _cameraBase.floor = GameObject.Find(colID).GetComponent<Transform>();
                    _playerBase.grounded = true;
                    break;
                }
                case "EnvObjects":
                {
                    var broke = false;
                    foreach (var obj in _rigidbodyManager.objects)
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
                    }

                    break;
                }
            }
        }
        else
        {
            switch (collision.transform.tag)
            {
                case "Ground":
                {
                    var colID = collision.gameObject.name;
                    _cameraBase.floor = GameObject.Find(colID).GetComponent<Transform>();
                    _playerBase.grounded = true;
                    break;
                }
                case "Item":
                    collision.collider.isTrigger = true;
                    collision.transform.GetComponent<Rigidbody>().useGravity = false;
                    break;
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Item")) return;
        
        var ıtemWorld = other.GetComponent<ItemWorld>();
        var ıtemMagnet = other.gameObject.GetComponent<ItemMagnet>();
        
        if(ınventory.GetItemList().Count > 5) return;

        _exists = false;
        foreach (var ıtem in ınventory.GetItemList())
        {
            if(other.gameObject.name.Equals(ıtem.GetGameObject().name)) _exists = true;
        }
        if(_exists) return;
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
}
