using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public enum ItemTypes
    {
        Plank, Cable, Nails, Iron, Stone
    }
    
    public ItemTypes ıtemTypes;
    
    [NonSerialized]public KeyCode keyCode;
    [NonSerialized] public Image slot;

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
    
    public Sprite GetSprite()
    {
        switch (ıtemTypes)
        {
            default:
            case ItemTypes.Plank: return InventoryUI.Instance.sprites[0];
            case ItemTypes.Cable: return InventoryUI.Instance.sprites[1];
            case ItemTypes.Nails: return InventoryUI.Instance.sprites[2];
            case ItemTypes.Iron: return InventoryUI.Instance.sprites[3];
            case ItemTypes.Stone: return InventoryUI.Instance.sprites[4];
        }
    }
}
