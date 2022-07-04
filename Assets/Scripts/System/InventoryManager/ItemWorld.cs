using System.Collections;
using System.Collections.Generic;
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
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _coll = GetComponent<BoxCollider>();
        _trans = GetComponent<Transform>();
    }

    public void SetItem(Item item)
    {
        this._ıtem = item;
        _meshFilter.sharedMesh = item.GetGameObject().GetComponent<MeshFilter>().sharedMesh;
        _meshRenderer.sharedMaterials = item.GetGameObject().GetComponent<MeshRenderer>().sharedMaterials;
        _trans.localScale = item.GetGameObject().GetComponent<Transform>().localScale;
        _trans.rotation = item.GetGameObject().GetComponent<Transform>().rotation;
        _coll.size = item.GetGameObject().GetComponent<BoxCollider>().size;
        //Rename();
    }
}