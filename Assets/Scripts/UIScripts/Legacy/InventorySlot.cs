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

    public static List<InventorySlot> slots;
    public static InventorySlot slot;

    private void Awake()
    {
        slot = this;
        _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void ToText()
    {
        _text.SetText(slot.slotClass + " " + slot.ıtemCount.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(slot.keyCode))
        {
            Debug.LogError("You have Selected " + slot.slotClass);
        }
    }
}
