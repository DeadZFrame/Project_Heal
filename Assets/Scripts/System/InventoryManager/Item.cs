using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class Item
{
    public enum ItemTypes
    {
        Plank, Cable, Nails, Iron, Stone
    }

    public ItemTypes ıtemTypes;
    public int amount;

    public GameObject GetGameObject()
    {
        switch (ıtemTypes)
        {
            default:
            case ItemTypes.Plank: return ItemAssets.Instance.materials[0];
            case ItemTypes.Cable: return ItemAssets.Instance.materials[1];
            case ItemTypes.Nails: return ItemAssets.Instance.materials[2];
            case ItemTypes.Iron: return ItemAssets.Instance.materials[3];
            case ItemTypes.Stone: return ItemAssets.Instance.materials[4];
        }
    }
}
