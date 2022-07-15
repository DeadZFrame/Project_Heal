using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public List<string> dialogue; 
    public List<string> tutorial;
    public TextMeshProUGUI dialogueUI;

    public Vector3 offset;

    [NonSerialized]public bool isWrited;
}
