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
    }

    private void Start()
    {
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
            bool broke = false;
            foreach (var obj in _rigidbodyManager.objects)
            {
                if (obj.constraints == RigidbodyConstraints.FreezeAll)
                {
                    if (!broke)
                    {
                        var parent = collision.transform.GetComponentInParent<Transform>().position;
                        ItemWorld.SpawnItemWorld(new Vector3(parent.x, parent.y, 1.5f), new Item{ıtemTypes = Item.ItemTypes.Plank, amount = 1});   
                    }
                    broke = true;
                }
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
