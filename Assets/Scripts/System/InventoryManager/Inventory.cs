using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    
    private List<Item> _ıtemList;

    public Inventory()
    {
        _ıtemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        bool ınInventory = false;
        foreach (Item ıtem in _ıtemList)
        {
            if (ıtem.ıtemTypes == item.ıtemTypes)
            {
                ıtem.amount += item.amount;
                ınInventory = true;
            }
        }
        if(!ınInventory)
            _ıtemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return _ıtemList;
    }

}