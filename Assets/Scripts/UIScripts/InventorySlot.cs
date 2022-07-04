using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [System.NonSerialized]public string slotClass = null;
    [System.NonSerialized]public int ıtemCount;
    [System.NonSerialized]public KeyCode keyCode;

    private InventoryManager _ınventoryManager;

    private void Awake()
    {
        _ınventoryManager = gameObject.GetComponentInParent<InventoryManager>();
    }
}
