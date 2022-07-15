using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private LevelManager _levelManager;
    private DialogManager _dialogManager;
    
    public Transform tutorialUI;
    public TextMeshProUGUI tutorialText;

    private void Awake()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
    }

    private void Start()
    {
        if (_levelManager.level == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex is (int)LevelManager.SceneIndex.Garage or (int)LevelManager.SceneIndex.Tutorial)
            {
                tutorialUI.gameObject.SetActive(true);
                tutorialText.gameObject.SetActive(true);
                
                if (SceneManager.GetActiveScene().buildIndex == (int)LevelManager.SceneIndex.Tutorial)
                {
                    var dialogues = _dialogManager.tutorial;
                    foreach (var dialogue in dialogues)
                    {
                        TextWriter.WriteText_Static(tutorialText, dialogue, .03f, true);
                        dialogues.Remove(dialogue);
                        break;
                    }

                    _pos = _levelManager.mailBox.position;
                }
                else
                {
                    TextWriter.WriteText_Static(tutorialText, _dialogManager.dialogue[1], .03f, true);   
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_levelManager.level == 0)
        {
            Track();
        }
    }

    public Transform tv, radio;
    private Vector3 _pos;
    private int _ındex = 0;
    private void Track()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)LevelManager.SceneIndex.Garage)
        {
            if (!_levelManager.selectedScene)
            {
                tutorialUI.position = Camera.main.WorldToScreenPoint(_levelManager.panel.position);
            }
            else
            {
                tutorialUI.position = Camera.main.WorldToScreenPoint(_levelManager.redButton.position);
                if (!_dialogManager.isWrited)
                {
                    TextWriter.WriteText_Static(tutorialText, _dialogManager.dialogue[3], .03f, true);
                    _dialogManager.isWrited = true;
                }
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == (int)LevelManager.SceneIndex.Tutorial)
        {
            var distance = Vector3.Distance(_levelManager.player.transform.position, tv.transform.position);
            if (distance < 4f)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _levelManager.pressedF = true;
                }
            }
            tutorialUI.position = Camera.main.WorldToScreenPoint(_pos);
            var dialogues = _dialogManager.tutorial;
            if (_levelManager.pressedF)
            {
                foreach (var dialogue in dialogues)
                {
                    TextWriter.WriteText_Static(tutorialText, dialogue, .03f, true);
                    dialogues.Remove(dialogue);
                    break;
                }

                _ındex += 1;
                if (_ındex > 1)
                {
                    _pos = radio.position;
                }
                else if(_ındex > 2)
                {
                    tutorialUI.gameObject.SetActive(false);
                }
                else
                {
                    _pos = tv.position;   
                }
                _levelManager.pressedF = false;
            }

            if (_levelManager.player.ınventory.GetItemList().Count > 0)
            {
                foreach (var dialogue in dialogues)
                {
                    TextWriter.WriteText_Static(tutorialText, dialogue, .03f, true);
                    dialogues.Remove(dialogue);
                    break;
                }

                _pos = tv.position;
            }
        }
    }
}
