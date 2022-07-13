using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform redButton ,panel;
    public Button interact;
    private Player _player;

    public GameObject levelMenu;

    private Vector3 _interactOffset;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name.Equals("Garage"))
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    
    public enum SceneIndex
    {
        MainMenu, Garage, Level01, Level02, Level03
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelMenu.SetActive(false);
            Time.timeScale = levelMenu.activeInHierarchy ? 0f : 1f;
        }

        if (SceneManager.GetActiveScene().name.Equals("Garage"))
        {
            BigRedButton();
            Panel();
        }
    }

    [NonSerialized] public int sceneIndex;
    [NonSerialized] public bool selectedScene = false;
    
    private void Panel()
    {
        var distance = Vector3.Distance(_player.transform.position, panel.transform.position);
        if (distance < 6f)
        {
            interact.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                levelMenu.SetActive(true);
                Time.timeScale = levelMenu.activeInHierarchy ? 0f : 1f;
            }
        }
    }
    
    private void BigRedButton()
    {
        var distance = Vector3.Distance(_player.transform.position, redButton.transform.position);
        if (distance < 4f && selectedScene)
        {
            interact.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadSceneAsync(sceneIndex);
            }
        }
        else
        {
            interact.gameObject.SetActive(false);
        }
    }
}
