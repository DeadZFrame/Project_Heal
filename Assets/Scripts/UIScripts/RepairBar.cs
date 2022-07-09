using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RepairBar : MonoBehaviour
{
    [NonSerialized] public Slider repairBar;
    private PlayerBase _playerBase;
    private void Awake()
    {
        repairBar = gameObject.GetComponent<Slider>();
        _playerBase = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();
    }

    private void Update()
    {
        if (repairBar.value < .99f)
        {
            if (repairBar.value == 0)
            {
                repairBar.gameObject.SetActive(false);
            }
            repairBar.value -= 0.07f * Time.deltaTime;
        }
        if(_playerBase.brokeObjects == null) return;
        repairBar.transform.position = Camera.main.WorldToScreenPoint(_playerBase.brokeObjects.transform.position);
    }

    /*private void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(_player.position);
    }*/
}
