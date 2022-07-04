using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RigidbodyManager : MonoBehaviour
{
    public Rigidbody[] objects;

    private void Awake()
    {
        objects = gameObject.GetComponentsInChildren<Rigidbody>();
    }
}
