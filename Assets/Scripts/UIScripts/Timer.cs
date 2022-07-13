using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float _timer;
    private bool _timeIsRunning;

    public TextMeshProUGUI timer;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level01"))
        {
            _timer = 300;
            _timeIsRunning = true;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level01"))
        {
            ManageTime();   
        }
    }

    private void ManageTime()
    {
        if (_timeIsRunning)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                DisplayTime(_timer);
            }
            else
            {
                Debug.Log("Time has run out!");
                _timer = 0;
                _timeIsRunning = false;
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
