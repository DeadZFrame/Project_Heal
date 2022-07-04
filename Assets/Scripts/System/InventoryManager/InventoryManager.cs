using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> ıtems;
    private GameObject[] _slots;
    private Image _slotIcon;

    private PlayerBase _playerBase;

    private bool _isEmpty = true;

    private void Awake()
    {
        _slotIcon = gameObject.GetComponentInChildren<Image>();
        _playerBase = GameObject.Find("Player").GetComponent<PlayerBase>();
    }

    private void Start()
    {
        ıtems = new List<GameObject>();
    }

    public void AddItem()
    {
        if (_playerBase.ıtem.transform.tag.Equals("Item"))
        {
            if (_isEmpty)
            {
                if (_slotIcon != null) _slotIcon.enabled = true;
                _isEmpty = false;
            }
            else
            {
                Instantiate(_slotIcon, gameObject.transform.parent.position, quaternion.identity);
                _slots = GameObject.FindGameObjectsWithTag("Slot");
                foreach (var slot in _slots)
                {
                    UnityEditor.GameObjectUtility.SetParentAndAlign(slot, gameObject);
                }
            }
            ıtems.Add(_playerBase.ıtem.gameObject);
        }
    }
}
