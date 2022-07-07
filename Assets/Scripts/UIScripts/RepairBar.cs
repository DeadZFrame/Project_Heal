using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RepairBar : MonoBehaviour
{
    [NonSerialized] public Slider repairBar;
    public bool repairing = false;
    private void Awake()
    {
        repairBar = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if (repairing)
        {
            repairBar.value += 0.3f * Time.deltaTime;
        }
        else
        {
            repairBar.value -= 0.1f * Time.deltaTime;
        }
    }
}
