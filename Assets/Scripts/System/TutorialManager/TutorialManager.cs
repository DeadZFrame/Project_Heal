using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private LevelManager _levelManager;
    public Transform tutorialUI;

    private void Awake()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Start()
    {
        tutorialUI.gameObject.SetActive(true);
    }

    private void Update()
    {
        Track();
    }

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
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == (int)LevelManager.SceneIndex.Tutorial)
        {
            tutorialUI.position = Camera.main.WorldToScreenPoint(_levelManager.mailBox.position);
        }
    }
}
