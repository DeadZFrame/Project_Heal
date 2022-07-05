using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item ıtem)
    {
        Transform transform = Instantiate(ItemAssets.Instance.materialWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(ıtem);

        return itemWorld;
    }

    private Item _ıtem;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private BoxCollider _coll;
    private Transform _trans;

    private void Awake()
    {
        _coll = GetComponent<BoxCollider>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _trans = GetComponent<Transform>();
    }

    private void SetItem(Item ıtem)
    {
        this._ıtem = ıtem;
        _meshFilter.sharedMesh = ıtem.GetGameObject().GetComponent<MeshFilter>().sharedMesh;
        _meshRenderer.sharedMaterials = ıtem.GetGameObject().GetComponent<MeshRenderer>().sharedMaterials;
        _trans.localScale = ıtem.GetGameObject().GetComponent<Transform>().localScale;
        _trans.rotation = ıtem.GetGameObject().GetComponent<Transform>().rotation;
        _coll.size = ıtem.GetGameObject().GetComponent<BoxCollider>().size;
        //Rename();
    }

    public Item GetItem()
    {
        return _ıtem;
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
    }
}