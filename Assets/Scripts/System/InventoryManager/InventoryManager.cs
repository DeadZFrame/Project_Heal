using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> ıtems;
    private List<KeyCode> _keyCodes;
    private GameObject[] _slots;
    
    private Image _slotIcon;

    private PlayerBase _playerBase;

    private bool _isEmpty = true, _isSame = false;

    private void Awake()
    {
        _slotIcon = gameObject.GetComponentInChildren<Image>();
        _playerBase = GameObject.Find("Player").GetComponent<PlayerBase>();
    }

    private void Start()
    {
        ıtems = new List<GameObject>();
        _keyCodes = new List<KeyCode>() { KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };
    }

    public void AddItem()
    {
        if (_playerBase.ıtem.transform.tag.Equals("Item"))
        {
            if (_isEmpty)
            {
                if (_slotIcon != null) _slotIcon.enabled = true;
                if (_slotIcon.enabled) _slotIcon.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                var slot = _slotIcon.GetComponent<InventorySlot>();
                slot.slotClass = _playerBase.ıtem.name;
                slot.ıtemCount += 1;
                slot.keyCode = KeyCode.Alpha1;
                _isEmpty = false;
            }
            else
            {
                ItemClassifier();
                if (!_isSame)
                {
                    Instantiate(_slotIcon, gameObject.transform.parent.position, quaternion.identity);
                    _slots = GameObject.FindGameObjectsWithTag("Slot");
                    foreach (var slot in _slots)
                    {
                        UnityEditor.GameObjectUtility.SetParentAndAlign(slot, gameObject);
                        
                        var _slot = slot.GetComponent<InventorySlot>();
                        if (_slot.slotClass == null)
                        {
                            _slot.slotClass = _playerBase.ıtem.name;
                            _slot.ıtemCount += 1;
                            foreach (var key in _keyCodes)
                            {
                                _slot.keyCode = key;
                                _keyCodes.Remove(key);
                                break;
                            }
                        }
                    }
                }
                
            }
            _isSame = false;
            ıtems.Add(_playerBase.ıtem.gameObject);
        }
    }

    private void ItemClassifier()
    {
        foreach (var ıtem in ıtems)
        {
            if (_playerBase.ıtem.name.Equals(ıtem.name))
            {
                _isSame = true;
                _slots = GameObject.FindGameObjectsWithTag("Slot");
                foreach (var slot in _slots)
                {
                    var _slot = slot.GetComponent<InventorySlot>();
                    if (_slot.slotClass == ıtem.name)
                    {
                        _slot.ıtemCount += 1;
                    }
                }
            }
        }
        
    }
}
