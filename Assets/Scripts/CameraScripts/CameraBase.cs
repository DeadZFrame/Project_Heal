using UnityEngine;

public class CameraBase : MonoBehaviour
{
    [System.NonSerialized]public Transform floor;
    private Transform _player;
    public Vector3 offset;
    private Vector3 _velocity = Vector3.zero;

    public float speed;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        floor = GameObject.Find("Floor").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 center = new Vector3(floor.position.x + _player.transform.position.x, 0 ,floor.position.z + _player.transform.position.z) / 2f;
        Vector3 cam = center + offset;
        Vector3 smooth = Vector3.SmoothDamp(transform.position, cam, ref _velocity, speed);
        transform.position = smooth;
    }
}
