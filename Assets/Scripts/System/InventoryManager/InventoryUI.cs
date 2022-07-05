using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _ınventory;
    private Transform _slotGroup;
    private Transform _slot;

    private void Awake()
    {
        _slotGroup = transform.Find("SlotGroup");
        _slot = _slotGroup.Find("Slot");
    }

    public void SetInventory(Inventory ınventory)
    {
        this._ınventory = ınventory;
    }

    private void RefreshInventory()
    {
        
    }
}
