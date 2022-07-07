using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Radius : MonoBehaviour
{
    [NonSerialized]public Collider obj;
    
    private void Awake()
    {
        gameObject.GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Item"))
        {
            obj = other;
        }
    }
}
