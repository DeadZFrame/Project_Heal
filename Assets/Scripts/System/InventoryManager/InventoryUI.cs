using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

        _ınventory.OnItemListChanged += Inventory_OnItemListChanged;
        
        RefreshInventory();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventory();
    }

    private void RefreshInventory()
    {
        foreach (Transform child in _slotGroup)
        {
            if (child == _slot) continue;
            Destroy(child.gameObject);
        }
        
        var x = 0;
        var y = 0;
        var slotSize = 50f;
        foreach (var ıtem in _ınventory.GetItemList())
        {
            RectTransform slotRectTransform = Instantiate(_slot, _slotGroup).GetComponent<RectTransform>();
            slotRectTransform.gameObject.SetActive(true);

            slotRectTransform.anchoredPosition = new Vector2(x * slotSize, y * slotSize);
            Image ımage = slotRectTransform.Find("Image").GetComponent<Image>();
            ımage.sprite = ıtem.GetSprite();

            x++;
            if (x <= 4) continue;
            x = 0;
            y++;

        }
    }
}