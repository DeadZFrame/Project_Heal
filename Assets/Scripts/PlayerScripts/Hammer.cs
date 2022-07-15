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
            SFX_Manager.instance.PlayArray("Hit");
            parent = obj.transform.parent;
            obj.gameObject.layer = LayerMask.NameToLayer("HitObj");
        }
        var objects = parent.GetComponentsInChildren<Rigidbody>();
        
        foreach (var obj in objects)
        {
            if (obj.constraints == RigidbodyConstraints.FreezeAll)
            {
                if (!broke && SceneManager.GetActiveScene().buildIndex is not (int)LevelManager.SceneIndex.Tutorial and not (int)LevelManager.SceneIndex.Level01)
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
                else if(!broke && SceneManager.GetActiveScene().buildIndex == (int)LevelManager.SceneIndex.Level01)
                {
                    if (!obj.transform.parent.name.Equals("Crate"))
                    {
                        if (obj.transform.name.Equals("Bowl"))
                        {
                            var vector = obj.transform.position;
                            ItemWorld.SpawnItemWorld(new Vector3(vector.x, vector.y, 1.5f), new Item{ıtemTypes = Item.ItemTypes.TeflonTape});
                        }
                        else if(obj.transform.parent.name.Equals("ChairSpawn"))
                        {
                            var parentVector = obj.transform.GetComponentInParent<Transform>().position;
                            ItemWorld.SpawnItemWorld(new Vector3(parentVector.x, parentVector.y, 1.5f), new Item{ıtemTypes = Item.ItemTypes.Cable});
                        }
                        else if(obj.transform.parent.name.Equals("CoffeTable"))
                        {
                            var parentVector = obj.transform.GetComponentInParent<Transform>().position;
                            ItemWorld.SpawnItemWorld(new Vector3(parentVector.x, parentVector.y, 1.5f), new Item{ıtemTypes = Item.ItemTypes.MetalPlate});
                        }
                        /*else if(obj.transform.parent.name.Equals("Crate1"))
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
                        }*/
                        else
                        {
                            var parentVector = obj.transform.GetComponentInParent<Transform>().position;
                            var random = Random.Range(0, 2);
                            var ıtem = random switch
                            {
                                0 => new Item { ıtemTypes = Item.ItemTypes.MetalPlate },
                                1 => new Item { ıtemTypes = Item.ItemTypes.TeflonTape },
                                _ => throw new ArgumentOutOfRangeException()
                            };
                            ItemWorld.SpawnItemWorld(new Vector3(parentVector.x, parentVector.y, 1.5f), ıtem);
                        }
                    }
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
