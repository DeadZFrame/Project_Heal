using System.Collections;
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
        StartCoroutine(DelayMagnet());
    }
    
    IEnumerator DelayMagnet()
    {
        yield return new WaitForSeconds(0.5f);
        var speed = 0.1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _player.position, speed + 30f * Time.deltaTime);
    }
}
