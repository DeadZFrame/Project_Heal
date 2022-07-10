using System.Collections;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(DelayMagnet());
        StartCoroutine(Fix());
    }

    private IEnumerator DelayMagnet()
    {
        yield return new WaitForSeconds(0.5f);
        var speed = 0.1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, speed + 30f * Time.deltaTime);
    }

    private IEnumerator Fix()
    {
        yield return new WaitForSeconds(1f);
        if (gameObject.GetComponent<BoxCollider>().bounds
            .Intersects(_player.transform.GetComponent<BoxCollider>().bounds))
        {
            var ıtemWorld = gameObject.GetComponent<ItemWorld>();
            _player.ınventory.AddItem(ıtemWorld.GetItem());
            ıtemWorld.DestroySelf();
        }
    }
}
