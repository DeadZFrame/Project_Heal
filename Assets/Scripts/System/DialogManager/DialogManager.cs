using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
        [System.NonSerialized]
    public List<TextWriterSingle> textWriterSingleList;
    [System.NonSerialized] public static DialogManager instance;
    [System.NonSerialized] public bool isWrited = false;


    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static void WriteText_Static(TextMeshProUGUI dialogText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd)
    {
        if (removeWriterBeforeAdd)
        {
            instance.RemoveWriter(dialogText);
        }
        instance.WriteText(dialogText, textToWrite, timePerCharacter, invisibleCharacters);
    }

    private void WriteText(TextMeshProUGUI dialogText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        textWriterSingleList.Add(new TextWriterSingle(dialogText, textToWrite, timePerCharacter, invisibleCharacters));
    }

    private void RemoveWriter(TextMeshProUGUI dialogueText)
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if (textWriterSingleList[i].GetProUGUI() == dialogueText)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
                isWrited = true;
            }
            else
            {
                isWrited = false;
            }
        }

    }

    public class TextWriterSingle
    {
        private TextMeshProUGUI dialogText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;

        public TextWriterSingle(TextMeshProUGUI dialogText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
        {
            this.dialogText = dialogText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            characterIndex = 0;
        }

        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }
                dialogText.SetText(text);

                if (characterIndex >= textToWrite.Length)
                {

                    return true;
                }
            }
            return false;
        }

        public TextMeshProUGUI GetProUGUI()
        {
            return dialogText;
        }
    }
}
