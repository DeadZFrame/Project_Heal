using System;
using System.Collections.Generic;
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
        _ıtemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return _ıtemList;
    }

}