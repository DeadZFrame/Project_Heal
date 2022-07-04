using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [System.NonSerialized]public string slotClass = null;
    [System.NonSerialized]public int ıtemCount;
    [System.NonSerialized]public KeyCode keyCode;
    
    private TextMeshProUGUI _text;

    private static InventorySlot _ınventorySlot;

    private void Awake()
    {
        _ınventorySlot = this;
        _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void ToText()
    {
        _text.SetText(_ınventorySlot.slotClass + " " + _ınventorySlot.ıtemCount.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Debug.LogError("You have Selected " + _ınventorySlot.slotClass);
        }
    }
}
