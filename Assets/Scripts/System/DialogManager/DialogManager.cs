using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public List<string> dialogue; 
    public TextMeshProUGUI dialogueUI;
    private Transform _player;

    public Vector3 offset;
    
    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        dialogueUI.gameObject.SetActive(true);
    }

    private void Start()
    {
        TextWriter.WriteText_Static(dialogueUI, dialogue[0], .05f, true);
    }

    private void Update()
    {
        dialogueUI.rectTransform.position = Camera.main.WorldToScreenPoint(_player.position) + offset;
    }
}
