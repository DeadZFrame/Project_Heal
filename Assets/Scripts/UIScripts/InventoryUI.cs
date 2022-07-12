using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    
    private Inventory _ınventory;

    public RectTransform[] slotGroup;
    private Image[] _slots, _slots2;
    
    public List<Image> slots;
    public Sprite[] sprites;

    public Sprite selectedSlot, normalSlot;

    private void Awake()
    {
        Instance = this;
        _slots = slotGroup[0].GetComponentsInChildren<Image>();
        _slots2 = slotGroup[1].GetComponentsInChildren<Image>();
        
        slots = new List<Image>();
        foreach (var slot in _slots)
        {
            slots.Add(slot);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var slotTemp = _slots2[0];
            if (slotTemp.sprite == selectedSlot)
            {
                slotTemp.sprite = normalSlot;
                return;
            }
            slotTemp.sprite = selectedSlot;
            foreach (var slot in _slots2)
            {
                if(slot == slotTemp) continue;
                slot.sprite = normalSlot;
            }
        }if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var slotTemp = _slots2[1];
            if (slotTemp.sprite == selectedSlot)
            {
                slotTemp.sprite = normalSlot;
                return;
            }
            slotTemp.sprite = selectedSlot;
            foreach (var slot in _slots2)
            {
                if(slot == slotTemp) continue;
                slot.sprite = normalSlot;
            }
        }if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var slotTemp = _slots2[2];
            if (slotTemp.sprite == selectedSlot)
            {
                slotTemp.sprite = normalSlot;
                return;
            }
            slotTemp.sprite = selectedSlot;
            foreach (var slot in _slots2)
            {
                if(slot == slotTemp) continue;
                slot.sprite = normalSlot;
            }
        }if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            var slotTemp = _slots2[3];
            if (slotTemp.sprite == selectedSlot)
            {
                slotTemp.sprite = normalSlot;
                return;
            }
            slotTemp.sprite = selectedSlot;
            foreach (var slot in _slots2)
            {
                if(slot == slotTemp) continue;
                slot.sprite = normalSlot;
            }
        }if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            var slotTemp = _slots2[4];
            if (slotTemp.sprite == selectedSlot)
            {
                slotTemp.sprite = normalSlot;
                return;
            }
            slotTemp.sprite = selectedSlot;
            foreach (var slot in _slots2)
            {
                if(slot == slotTemp) continue;
                slot.sprite = normalSlot;
            }
        }
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
        foreach (var ıtem in _ınventory.GetItemList())
        {
            if(ıtem.slot != null) continue;
            
            foreach (var slot in slots)
            {
                ıtem.slot = slot;
                ıtem.slot.sprite = ıtem.GetSprite();
                slots.Remove(slot);
                break;
            }
            foreach (var key in ItemAssets.Instance.keyCodes)
            {
                ıtem.keyCode = key;
                ItemAssets.Instance.keyCodes.Remove(key);
                break;
            }
        }
    }
}