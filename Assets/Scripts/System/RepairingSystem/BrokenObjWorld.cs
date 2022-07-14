using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenObjWorld : MonoBehaviour
{
    private RepairManager _repairManager;
    private void Awake()
    {
        _repairManager = GameObject.Find("RepairManager").GetComponent<RepairManager>();
    }

    private void Update()
    {
        foreach (var part in _repairManager.missingParts)
        {
            var uı = part.GetComponentsInChildren<Transform>();
            if (uı.Length == 1)
            {
                gameObject.layer = LayerMask.NameToLayer("Broken");
            }
        }
    }
}
