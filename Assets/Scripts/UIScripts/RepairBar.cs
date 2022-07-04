using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairBar : MonoBehaviour
{
    private Slider _repairBar;
    public bool repairing = false;
    private void Awake()
    {
        _repairBar = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if (repairing)
        {
            _repairBar.value += 0.3f * Time.deltaTime;
        }
        else
        {
            _repairBar.value -= 0.1f * Time.deltaTime;
        }
    }
}
