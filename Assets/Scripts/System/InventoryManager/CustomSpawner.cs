using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomSpawner : MonoBehaviour
{
    public Item ıtem;

    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, ıtem);
        Destroy(gameObject);
    }
}
