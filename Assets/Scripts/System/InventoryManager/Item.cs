using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public enum ItemTypes
    {
        Cable,
        MetalPlate,
        TeflonTape,
        ElectricTape,
        Switch,
    }
    
    public ItemTypes ıtemTypes;
    
    [NonSerialized] public KeyCode keyCode;
    [NonSerialized] public Image slot;

    public GameObject GetGameObject()
    {
        switch (ıtemTypes)
        {
            default:
            case ItemTypes.Cable: return ItemAssets.Instance.materials[0];
            case ItemTypes.MetalPlate: return ItemAssets.Instance.materials[1];
            case ItemTypes.TeflonTape: return ItemAssets.Instance.materials[2];
            case ItemTypes.ElectricTape: return ItemAssets.Instance.materials[3];
            case ItemTypes.Switch: return ItemAssets.Instance.materials[4];
        }
    }
    
    public Sprite GetSprite()
    {
        switch (ıtemTypes)
        {
            default:
            case ItemTypes.Cable: return InventoryUI.Instance.sprites[0];
            case ItemTypes.MetalPlate: return InventoryUI.Instance.sprites[1];
            case ItemTypes.TeflonTape: return InventoryUI.Instance.sprites[2];
            case ItemTypes.ElectricTape: return InventoryUI.Instance.sprites[3];
            case ItemTypes.Switch: return InventoryUI.Instance.sprites[4];
        }
    }
}
