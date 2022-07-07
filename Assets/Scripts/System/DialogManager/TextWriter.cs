using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
        [System.NonSerialized]
    public List<TextWriterSingle> textWriterSingleList;
    [System.NonSerialized] public static TextWriter ınstance;

    private void Awake()
    {
        ınstance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static void WriteText_Static(TextMeshProUGUI dialogUI, string textToWrite, float timePerCharacter, bool ınvisibleCharacters)
    {
        ınstance.RemoveWriter(dialogUI);
        ınstance.WriteText(dialogUI, textToWrite, timePerCharacter, ınvisibleCharacters);
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
            }
            else
            {
                
            }
        }
    }

    public class TextWriterSingle
    {
        private TextMeshProUGUI _dialogText;
        private string _textToWrite;
        private int _characterIndex;
        private float _timePerCharacter;
        private float _timer;
        private bool _ınvisibleCharacters;

        public TextWriterSingle(TextMeshProUGUI dialogText, string textToWrite, float timePerCharacter, bool ınvisibleCharacters)
        {
            this._dialogText = dialogText;
            this._textToWrite = textToWrite;
            this._timePerCharacter = timePerCharacter;
            this._ınvisibleCharacters = ınvisibleCharacters;
            _characterIndex = 0;
        }

        public bool Update()
        {
            _timer -= Time.deltaTime;
            while (_timer <= 0f)
            {
                _timer += _timePerCharacter;
                _characterIndex++;
                string text = _textToWrite.Substring(0, _characterIndex);
                if (_ınvisibleCharacters)
                {
                    text += "<color=#00000000>" + _textToWrite.Substring(_characterIndex) + "</color>";
                }
                _dialogText.SetText(text);

                if (_characterIndex >= _textToWrite.Length)
                {

                    return true;
                }
            }
            return false;
        }

        public TextMeshProUGUI GetProUGUI()
        {
            return _dialogText;
        }
    }
}
