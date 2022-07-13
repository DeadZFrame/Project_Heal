using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            Time.timeScale = pauseMenu.activeInHierarchy ? 0f : 1f;
        }
    }

    public void CheckTime()
    {
        Time.timeScale = _levelManager.levelMenu.activeInHierarchy ? 0f : 1f;
        Time.timeScale = pauseMenu.activeInHierarchy ? 0f : 1f;
    }

    public void NewGame()
    {
        SceneManager.LoadScene((int)LevelManager.SceneIndex.Garage);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ReturnToGarage()
    {
        SceneManager.LoadScene((int)LevelManager.SceneIndex.Garage);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void LoadTutorial()
    {
        _levelManager.sceneIndex = (int)LevelManager.SceneIndex.Level01;
        _levelManager.selectedScene = true;
        _levelManager.levelMenu.SetActive(false);
    }

    public void LoadLevel01()
    {
        _levelManager.sceneIndex = (int)LevelManager.SceneIndex.Level02;
        _levelManager.selectedScene = true;
        _levelManager.levelMenu.SetActive(false);
    }

    public void LoadLevel02()
    {
        _levelManager.sceneIndex = (int)LevelManager.SceneIndex.Level03;
        _levelManager.selectedScene = true;
        _levelManager.levelMenu.SetActive(false);
    }
}
