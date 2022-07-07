using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RepairBar : MonoBehaviour
{
    [NonSerialized] public Slider repairBar;
    private Transform _player;
    private void Awake()
    {
        repairBar = gameObject.GetComponent<Slider>();
        _player = GameObject.Find("Player").transform;
    }

    /*private void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(_player.position);
    }*/
}
