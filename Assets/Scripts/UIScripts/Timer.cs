using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [NonSerialized]public float time;
    [NonSerialized]public bool timeIsRunning;

    public TextMeshProUGUI timer;

    private UIManager _uıManager;

    private void Awake()
    {
        _uıManager = GameObject.Find("UIManager").GetComponent<UIManager>();

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)LevelManager.SceneIndex.Tutorial)
        {
            time = 180;
        }
        else if(SceneManager.GetActiveScene().buildIndex == (int)LevelManager.SceneIndex.Level01)
        {
            time = 300;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex is not ((int)LevelManager.SceneIndex.MainMenu and not (int)LevelManager.SceneIndex.CutScene and not (int)LevelManager.SceneIndex.Garage))
        {
            ManageTime();   
        }
    }

    private void ManageTime()
    {
        if (timeIsRunning)
        {
            if (time > 0.1f)
            {
                time -= Time.deltaTime;
                DisplayTime(time);
            }
            else
            {
                Debug.Log("Time has run out!");
                time = 0.05f;
                timeIsRunning = false;
                _uıManager.gameOver.SetActive(true);
            }
        }
    }

    private void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timer.SetText($"{minutes:00}:{seconds:00}");
    }
}
