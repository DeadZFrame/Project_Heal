using System;
using UnityEngine;
using UnityEngine.UI;

public class RepairManager : MonoBehaviour
{
    private Player _player;
    
    [NonSerialized] public GameObject[] brokenObjects;
    public GameObject[] missingParts;
    
    public Button interact;
    public Button throwUI;
    
    [NonSerialized]public Image[] ımages;

    public Vector3 offset;

    public enum Objects
    {
        TV
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        brokenObjects = GameObject.FindGameObjectsWithTag("Broken");
        
        foreach (var part in missingParts)
        {
            ımages = part.GetComponentsInChildren<Image>();
        }
        
        foreach (var obj in brokenObjects)
        {
            obj.AddComponent<BrokenObjWorld>();
            if(!obj.name.Equals("TV"))
            {
                missingParts[(int)Objects.TV].transform.position = Camera.main.WorldToScreenPoint(obj.transform.position);
            }
        }
    }

    private void Update()
    {
        ManageUI();
        throwUI.gameObject.SetActive(_player.ınventory.toggled);
    }

    [NonSerialized]public float distance;

    private void ManageUI()
    {
        foreach (var obj in brokenObjects)
        {
            distance = Vector3.Distance(_player.transform.position, obj.transform.position);
            foreach (var part in missingParts)
            {
                if (distance < 4f)
                {
                    if (!part.activeInHierarchy && part.GetComponentsInChildren<Transform>().Length != 0)
                    {
                        interact.gameObject.SetActive(true);
                    }
                    
                    part.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position + offset);

                    if (!Input.GetKeyDown(KeyCode.F)) return;
                    part.SetActive(true);
                    interact.gameObject.SetActive(false);
                }
                else
                {
                    if (part.activeInHierarchy)
                    {
                        part.SetActive(false);
                    }

                    interact.gameObject.SetActive(false);
                }   
            }
        }
    }
}
