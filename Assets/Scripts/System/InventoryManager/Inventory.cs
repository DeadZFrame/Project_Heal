using System;
using System.Collections.Generic;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    
    private List<Item> _ıtemList;

    [NonSerialized] public bool toggled = false;

    public Inventory()
    {
        _ıtemList = new List<Item>();
    }

    public void AddItem(Item ıtem)
    {
        _ıtemList.Add(ıtem);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item ıtem)
    {
        _ıtemList.Remove(ıtem);
        
        InventoryUI.Instance.slots.Insert(0, ıtem.slot);
        ıtem.slot.sprite = InventoryUI.Instance.normalSlot;
        ıtem.slot = null;
        ItemAssets.Instance.keyCodes.Insert(0, ıtem.keyCode);
    }

    public void ToggleItem()
    {
        toggled = !toggled;
        //Animation
    }

    public List<Item> GetItemList()
    {
        return _ıtemList;
    }

}