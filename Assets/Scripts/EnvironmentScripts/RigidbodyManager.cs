using System;
using UnityEngine;

public class RigidbodyManager : MonoBehaviour
{
    [NonSerialized]public Rigidbody[] objects;

    public void Initialize()
    {
        for (var i = 0; i < objects.Length; i++)
        {
            objects[i] = null;
        }
    }
}