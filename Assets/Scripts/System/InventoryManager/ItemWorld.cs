using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item ıtem)
    {
        var transform = Instantiate(ItemAssets.Instance.materialWorld, position, Quaternion.identity);

        var ıtemWorld = transform.GetComponent<ItemWorld>();
        ıtemWorld.SetItem(ıtem);

        return ıtemWorld;
    }

    private Item _ıtem;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private BoxCollider _coll;
    private Transform _trans;
    private Rigidbody _rb;

    private void Awake()
    {
        _coll = GetComponent<BoxCollider>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _trans = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        _repairManager = GameObject.Find("RepairManager").GetComponent<RepairManager>();
    }

    private void Start()
    {
        StartCoroutine(CanBeTaken(true));
        var random = Random.Range(0, 8);
        var vector = random switch
        {
            0 => Vector3.back, 1 => Vector3.forward, 2 => Vector3.left, 3 => Vector3.right,
            4 => Vector3.forward + Vector3.left, 5 => Vector3.forward + Vector3.right, 
            6 => Vector3.back + Vector3.left, 7 => Vector3.back + Vector3.right,
            _ => throw new ArgumentOutOfRangeException()
        };

        _rb.AddForce(vector * 5, ForceMode.Impulse);
    }

    private void SetItem(Item ıtem)
    {
        _ıtem = ıtem;
        _meshFilter.sharedMesh = ıtem.GetGameObject().GetComponent<MeshFilter>().sharedMesh;
        _meshRenderer.sharedMaterials = ıtem.GetGameObject().GetComponent<MeshRenderer>().sharedMaterials;
        _trans.localScale = ıtem.GetGameObject().GetComponent<Transform>().localScale;
        _trans.rotation = ıtem.GetGameObject().GetComponent<Transform>().rotation;
        _coll.size = ıtem.GetGameObject().GetComponent<BoxCollider>().size;
        gameObject.name = ıtem.GetGameObject().name;
    }

    public Item GetItem()
    {
        return _ıtem;
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
    }

    private RepairManager _repairManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.tag.Equals("Broken")) return;
        foreach (var obj in _repairManager.brokenObjects)
        {
            if (!collision.transform.name.Equals(obj.name)) continue;
            foreach (var uı in _repairManager.ımages)
            {
                if (!uı.name.Equals(gameObject.name)) continue;
                Destroy(uı.gameObject);
                DestroySelf();
            }
        }
    }

    [NonSerialized]public bool canBeTaken = false;
    private IEnumerator CanBeTaken(bool var)
    {
        yield return new WaitForSeconds(1f);
        canBeTaken = var;
    }
}