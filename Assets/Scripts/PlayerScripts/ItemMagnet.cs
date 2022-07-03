using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    private List<GameObject> items = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Item"))
        {
            items.Add(other.gameObject);
        }
        Magnet();
    }

    private void Magnet()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].gameObject.GetComponent<ItemWorld>() == null)
                items[i].gameObject.AddComponent<ItemWorld>();
        }
    }
}
