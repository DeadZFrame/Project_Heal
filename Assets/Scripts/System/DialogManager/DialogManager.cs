using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public List<string> dialogue;
    private TextMeshProUGUI _dialogueUI;
    private Transform _player;

    public Vector3 offset;
    
    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _dialogueUI = GameObject.Find("DialogueUI").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        TextWriter.WriteText_Static(_dialogueUI, dialogue[0], .05f, true);
    }

    private void Update()
    {
        _dialogueUI.rectTransform.position = Camera.main.WorldToScreenPoint(_player.position) + offset;
    }
}
