using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    private Transform _player;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector3 smooth = Vector3.SmoothDamp(gameObject.transform.position, _player.position, ref _velocity, 0.75f);
        gameObject.transform.position = smooth;
    }
}
