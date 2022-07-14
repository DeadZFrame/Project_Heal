using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Hammer : MonoBehaviour
{
    public float attackRange;
    public float force;
    public LayerMask hitObj, brokenObj, breakableObj;

    private Player _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Break()
    {
        var broke = false;
        var breakableobj = Physics.OverlapSphere(transform.position, attackRange, breakableObj);
        if(breakableobj == null) return;
        var parent = transform;
        foreach (var obj in breakableobj)
        {
            parent = obj.transform.parent;
            obj.gameObject.layer = LayerMask.NameToLayer("HitObj");
        }
        var objects = parent.GetComponentsInChildren<Rigidbody>();
        
        foreach (var obj in objects)
        {
            if (obj.constraints == RigidbodyConstraints.FreezeAll)
            {
                if (!broke && SceneManager.GetActiveScene().buildIndex != (int)LevelManager.SceneIndex.Tutorial)
                {
                    var parentVector = obj.transform.GetComponentInParent<Transform>().position;
                    var random = Random.Range(0, 5);
                    var ıtem = random switch
                    {
                        0 => new Item { ıtemTypes = Item.ItemTypes.Cable },
                        1 => new Item { ıtemTypes = Item.ItemTypes.Switch },
                        2 => new Item { ıtemTypes = Item.ItemTypes.ElectricTape },
                        3 => new Item { ıtemTypes = Item.ItemTypes.MetalPlate },
                        4 => new Item { ıtemTypes = Item.ItemTypes.TeflonTape },
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    ItemWorld.SpawnItemWorld(new Vector3(parentVector.x, parentVector.y, 1.5f), ıtem);
                }
                else if (!broke && SceneManager.GetActiveScene().buildIndex == (int)LevelManager.SceneIndex.Tutorial && obj.transform.parent.name.Equals("Radio"))
                {
                    var parentVector = obj.transform.GetComponentInParent<Transform>().position;
                    ItemWorld.SpawnItemWorld(new Vector3(parentVector.x, parentVector.y, 1.5f), new Item{ıtemTypes = Item.ItemTypes.Cable});
                }

                broke = true;
            }
            obj.constraints = RigidbodyConstraints.None;
            /*var objCollider = obj.GetComponent<BoxCollider>();
            if (objCollider.bounds
                .Intersects(_player.GetComponent<BoxCollider>().bounds))
            {
                objCollider.isTrigger = true;
            }*/
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
