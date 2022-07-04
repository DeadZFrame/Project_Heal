using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    private List<GameObject> _ıtems = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Item"))
        {
            _ıtems.Add(other.gameObject);
        }
        Magnet();
    }

    private void Magnet()
    {
        foreach (var ıtem in _ıtems)
        {
            if(ıtem.gameObject.GetComponent<ItemWorld>() == null)
                ıtem.gameObject.AddComponent<ItemWorld>();
        }
    }
}
