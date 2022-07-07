using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var key in keyCodes)
        {
            keyData.Add(key);
        }
    }

    public Transform materialWorld;
    
    public GameObject[] materials;
    public List<KeyCode> keyCodes;

    public List<KeyCode> keyData;
}
