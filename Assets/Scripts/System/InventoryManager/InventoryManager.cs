using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    private GameObject _inventory;
    private Button _button;

    private PlayerBase _playerBase;
    private ItemMagnet _ıtemMagnet;

    private bool _isEmpty = true;

    private void Awake()
    {
        _playerBase = GameObject.Find("Player").GetComponent<PlayerBase>();
        _ıtemMagnet = GameObject.Find("Radius").GetComponent<ItemMagnet>();
    }

    private void AddItem()
    {
        if (_playerBase.item.tag.Equals("item"))
        {
            if (_isEmpty)
            {
                _isEmpty = false;
                _button = GameObject.Find("Button").GetComponent<Button>();
                _button.gameObject.SetActive(true);
            }
            
        }
    }
}
