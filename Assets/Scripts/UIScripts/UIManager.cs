using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
        
        Time.timeScale = pauseMenu.activeInHierarchy ? 0f : 1f;
    }

    public void ReturnToGarage()
    {
        SceneManager.LoadScene("Garage");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel01()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel02()
    {
        SceneManager.LoadScene(3);
    }
}
