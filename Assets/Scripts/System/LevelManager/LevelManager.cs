using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform redButton ,panel, mailBox;
    public Button interact;
    private Player _player;

    public GameObject levelMenu;

    private Vector3 _interactOffset;

    private void Awake()
    {
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

        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Garage)
        {
            BigRedButton();
            Panel();
        }
        else if(SceneManager.GetActiveScene().buildIndex is not ((int)SceneIndex.MainMenu or (int)SceneIndex.Garage))
        {
            MailBox();
        }
    }

    private int _totalStars;
    [NonSerialized] public int starsForThisLevel;
    private void ManageStars()
    {
        
    }

    [NonSerialized] public int sceneIndex;
    [NonSerialized] public bool selectedScene = false;

    private void MailBox()
    {
        var distance = Vector3.Distance(_player.transform.position, mailBox.position);
        if (distance < 4f)
        {
            interact.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Open MailBox menu and set Time.scale to zero
            }
        }
        else
        {
            interact.gameObject.SetActive(false);
        }
    }
    
    private void Panel()
    {
        var distance = Vector3.Distance(_player.transform.position, panel.position);
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
        var distance = Vector3.Distance(_player.transform.position, redButton.position);
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
