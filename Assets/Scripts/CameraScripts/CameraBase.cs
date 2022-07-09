using System;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
    [NonSerialized]public Transform floor;
    private Transform _player;
    public Vector3 offset;
    private Vector3 _velocity = Vector3.zero;

    public float speed;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if(floor == null) return;
        var fPos = floor.position;
        var pPos = _player.position;
        var center = new Vector3(fPos.x + pPos.x, 0 ,fPos.z + pPos.z) / 2f;
        var cam = center + offset;
        var smooth = Vector3.SmoothDamp(transform.position, cam, ref _velocity, speed);
        transform.position = smooth;
    }
}
